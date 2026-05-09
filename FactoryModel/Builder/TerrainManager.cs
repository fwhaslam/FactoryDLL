// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models;
using FactoryModel.Models.Constants;

using static FactoryModel.Models.Constants.FacilityTypeEnum;
using static FactoryModel.Models.Constants.FacilityTypeInfo;

namespace FactoryModel.Builder {

    /// <summary>
    ///  Used to update and change a Terrain.
    ///  As tiles are added to a 'change', they are modified for display.
    /// </summary>
    public class TerrainManager {

        public Terrain Map {  get; set; } 

        public FacilityPlan Plan {  get; set; } = new FacilityPlan();

        public Tile LastTile { get; set; } = null;

//=====================================================================================================================

        internal void Reset() {
            Plan.Clear();
            LastTile = null;
        }

        public void StartChange() {
            Reset();
        }

        public void CancelChange() {
            Reset();
        }

        /// <summary>
        /// Facility values are copied into map, the plan is cleared.
        /// </summary>
        public void ApplyChange() {

            foreach ( var tile in Plan.Values ) {
                var loc = tile.Loc;
                Map.Grid[ loc.X, loc.Y ].Facility = tile.Facility;
            }

            Reset();
        }

//=====================================================================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="facility">Template for first tile.  Ignored as we drag belts around.</param>
        /// <returns>List[Where] - locations changed by this change </returns>
        public List<Where> SubmitBeltToPlan( Where loc ) {

            var touched = new List<Where>();
            if (!Map.InBounds(loc)) return touched;

            var tile = Map.Grid[loc.X,loc.Y];

            // cannot over-write existing facillities
            if (tile.Facility!=FacilityTypeEnum.Empty) return touched;

            // some terrain/facility combinations are illegal
            if (tile.Type==TileTypeEnum.Sea) return touched;

            // add change to plan
            var work = new Tile(loc,FacilityTypeEnumInfo.DEFAULT_BELT_ENUM);
            Plan.Put( work );
            touched.Add(loc);

            // cleanup
            if (LastTile!=null) touched.Add( LastTile.Loc );
            FixBeltLinks( LastTile, work );
            LastTile = work;
 
           return touched;
        }

        public bool ContainsKey( Where loc ) {
            return Plan.ContainsKey(loc);
        }

        public Tile Get( Where loc ) {
            return Plan[loc];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List[Where] - locations changed by this change </returns>
        public List<Where> RemoveFromPlan( Where loc ) {
            
            var touched = new List<Where>();
            if (!Plan.ContainsKey(loc)) return touched;
            if (!Map.InBounds(loc)) return touched;

            var found = Plan.RemoveAt( loc );
            if (found!=null) touched.Add( loc );
            return touched;
        }


//=====================================================================================================================

        internal static readonly Where NORTH_STEP = new Where( 0, +1 );
        internal static readonly Where SOUTH_STEP = new Where( 0, -1 );

        internal static readonly Where EAST_STEP = new Where( +1, 0 );
        internal static readonly Where WEST_STEP = new Where( -1, 0 );

        internal bool IsAdjacent( Where diff ) {
            if (diff==null) return false;
            return ( Math.Abs(diff.X) + Math.Abs(diff.Y) ) == 1;
        }

        internal void FixBeltLinks( Tile lastTile, Tile currentTile ) {

            var diff = ( lastTile==null ? null : lastTile.Loc.Minus( currentTile.Loc ) );

            // last tile is missing or not adjacent: adjust one tile
            if ( !IsAdjacent(diff) ) {
                MergeBelt( currentTile, null );
            }
            // last tile is present and adjacent: adjust two tiles
            else {
                MergeBelt( currentTile, diff );
                Plan.Put( currentTile );

                MergeBelt( lastTile, null );
                Plan.Put( lastTile );
            }
        }

        /// <summary>
        /// Use information from adjacent tiles to chose belt structure.
        /// </summary>
        /// <param name="work"></param>
        /// <param name="drag"></param>
        internal void MergeBelt( Tile work, Where drag ) {

            var loc = work.Loc;

            var northLink = GetLink( loc.X, loc.Y+1, (t) => FacilityTypeInfo.InverseSouthLink(t.Facility) );   // NORTH_STEP
            var southLink = GetLink( loc.X, loc.Y-1, (t) => FacilityTypeInfo.InverseNorthLink(t.Facility) );   // SOUTH_STEP
            var eastLink = GetLink( loc.X+1, loc.Y, (t) => FacilityTypeInfo.InverseWestLink(t.Facility) ); // EAST_STEP
            var westLink = GetLink( loc.X-1, loc.Y, (t) => FacilityTypeInfo.InverseEastLink(t.Facility) ); // WEST_STEP

            // direction to previous tile, dragging
            if (drag!=null) { 
                if ( EAST_STEP.Equals(drag) ) eastLink = FacilityTypeInfo.IN_LINK;
                if ( WEST_STEP.Equals(drag) ) westLink = FacilityTypeInfo.IN_LINK;
                if ( NORTH_STEP.Equals(drag) ) northLink = FacilityTypeInfo.IN_LINK;
                if ( SOUTH_STEP.Equals(drag) ) southLink = FacilityTypeInfo.IN_LINK;
            }

            var pick = FacilityTypeInfo.FacilityByDir[ northLink, southLink, eastLink, westLink ];
            work.Facility = pick;
        }


        internal int GetLink( int x,int y, Func<Tile,int> getter ) {
            var key = new Where(x,y);
            if (!Map.InBounds(key)) return FacilityTypeInfo.NO_LINK;
            if (Plan.ContainsKey(key)) return getter( Plan[key] );
            return getter( Map.Grid[x,y] );
        }
    }
}
