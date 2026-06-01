// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FactoryModel.Models;
using FactoryModel.Models.Constants;
using FactoryModel.Models.Sites;

using static FactoryModel.Models.Constants.LinkType;
using static FactoryModel.Models.Constants.TileTypeInfo;

namespace FactoryModel.Builder {

    /// <summary>
    ///  Used to update and change a Terrain.
    ///  As tiles are added to a 'change', they are modified for display.
    /// </summary>
    public class BeltSiteManager {

        public Terrain Map {  get; set; } 

        public ConstructionPlan Plan {  get; set; } = new ConstructionPlan();

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
        /// Site values are copied into map, then the plan is cleared.
        /// If a Site is going to be removed, then the plan contains an EmptySite.
        /// </summary>
        /// <returns>List[Where] location of all alterred tiles.</returns>
        public List<Where> ApplyChange() {

            var changeLocs = Plan.Keys.ToList();

            foreach ( var planTile in Plan.Values ) {
                var loc = planTile.Loc;
                Map.Grid[ loc.X, loc.Y ].Site = planTile.Site;
            }

            Reset();

            return changeLocs;
        }

//=====================================================================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loc"></param>
        /// <returns>List[Where] - locations changed by this change </returns>
        public List<Where> SubmitBeltToPlan( Where loc ) {

            var touched = new List<Where>();
            if (!Map.InBounds(loc)) return touched;

            // we can only make changes over empty or existing belts.
            // plan ONLY contains Belt sites for this manager.
            var tile = Map.Grid[loc.X,loc.Y];
            if (NoBuildBelt( tile.Type )) return touched;

            var siteGroup = tile.Site.Group;
            if (siteGroup!=SiteGroup.Empty && siteGroup!=SiteGroup.Belt) return touched;

            // setup plan tile for fixing links.
            var work = Plan.FixBeltTile( loc, SiteTypeInfo.DEFAULT_BELT_ENUM );
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

            var northLink = GetLink( loc.X, loc.Y+1, (t) => SiteTypeInfo.InverseSouthLink(t.Site) );   // NORTH_STEP
            var southLink = GetLink( loc.X, loc.Y-1, (t) => SiteTypeInfo.InverseNorthLink(t.Site) );   // SOUTH_STEP
            var eastLink = GetLink( loc.X+1, loc.Y, (t) => SiteTypeInfo.InverseWestLink(t.Site) ); // EAST_STEP
            var westLink = GetLink( loc.X-1, loc.Y, (t) => SiteTypeInfo.InverseEastLink(t.Site) ); // WEST_STEP

            // direction to previous tile, dragging
            if (drag!=null) { 
                if ( EAST_STEP.Equals(drag) ) eastLink = IN_LINK;
                if ( WEST_STEP.Equals(drag) ) westLink = IN_LINK;
                if ( NORTH_STEP.Equals(drag) ) northLink = IN_LINK;
                if ( SOUTH_STEP.Equals(drag) ) southLink = IN_LINK;
            }

            var pick = SiteTypeInfo.SiteByDir[ (int)northLink, (int)southLink, (int)eastLink, (int)westLink ];
            work.Site = new BeltSite() { Type  = pick };
        }


        internal LinkType GetLink( int x,int y, Func<Tile,LinkType> getter ) {
            var key = new Where(x,y);
            if (!Map.InBounds(key)) return NO_LINK;
            if (Plan.ContainsKey(key)) return getter( Plan[key] );
            return getter( Map.Grid[x,y] );
        }
    }
}
