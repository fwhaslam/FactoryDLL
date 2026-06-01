// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;

using FactoryModel.Models;
using FactoryModel.Models.Constants;
using FactoryModel.Models.Sites;
using FactoryModel.Tools;

using static FactoryModel.Models.Constants.TileType;
using static FactoryModel.Models.Constants.SiteType;
using static FactoryModel.Models.Constants.RuleType;
using static FactoryModel.Models.Constants.TileTypeInfo;

namespace FactoryModel.Builder {

    public class MissionBuilder {

        public RandTool Rand { get; set; } = new RandTool( Environment.TickCount );

        public Mission Mission { get; set; }

        internal Terrain Terrain {  get { return Mission.Terrain; } }

        internal Market Market {  get { return Mission.Market; } }

        public string ToDisplay() {
            return Mission.ToDisplay();
        }

        public void Build( MissionConfig config ) { 

            this.Mission = new Mission() {
                Config = config
            };

            // create terrain
            var tb = new TerrainBuilder() {
                Rand = Rand,
                Size = config.Size
            };
            tb.Build();
            Mission.Terrain = tb.Map;


            // create a market sized for the map.
            var volume = MeasureVolume();
            var chainCount = volume / 150;
            var mb = new MarketBuilder() { 
                Limit = chainCount 
            };
            mb.Build();
            Mission.Market = mb.Trade;

            // lay down events tied to start/end of chains
            var eventPts = RandomEventPoints( chainCount );

            // sort slightly so the first two points are 'close' to each other
            MakeFirstPointsClose( eventPts );

            // link chain rules to eventPts
            for (int ix=0;ix<chainCount;ix++) {

                // add make event
                var makeRule = Market.Chains[ix].FirstRule;
                var makePt = eventPts[ ix*2 ];
                AddEvent( makeRule, makePt );

                // add vend event
                var vendRule = Market.Chains[ix].LastRule;
                var vendPt = eventPts[ ix*2 + 1 ];
                AddEvent( vendRule, vendPt );

            }

            var display = eventPts.Aggregate( "", (show,info) => show+"\n"+info.ToDisplay() );
//Console.WriteLine("MISSION POINTS = "+display);
        }

//======================================================================================================================

        /// <summary>
        /// How many tiles can be useed for constrution ?
        /// 
        /// Swamps are 'half' usable.  Peaks and Sea are NOT usable.
        /// 
        /// </summary>
        /// <returns></returns>
        internal int MeasureVolume() {

            int sites = 0;
            int belts = 0;

            for (int ix=0;ix<Terrain.Wide;ix++) {
                for (int iy=0;iy<Terrain.Tall;iy++) {
                    var type = Terrain.Grid[ix,iy].Type;

                    if (IsBuildSite(type)) sites++;
                    if (IsBuildBelt(type)) belts++;
                }
            }

            // tiles that allow belts but not sites are 1/2 as valuable,
            // so averaging the tile counts gives a decent measure of volume.
            return ( sites + belts ) / 2;
        }

//======================================================================================================================

        /// <summary>
        /// Assume that all Events Are 2x2 tiles and must occupy one type of terrain.
        ///     ^^ too much, assume 1x1 for now.
        /// 
        /// Each selected location blocks out an area around the point.
        /// 
        /// 
        /// </summary>
        public List<Where> RandomEventPoints( int chainCount ) {

            var validLocs = new List<Where>();

            var pickLocs = new List<Where>();

            // initial list of valid tiles
            ScanMap( (t,p) => {
                if (t.Type!=Sea) validLocs.Add(p);
            });


            // pick random tiles
            while ( pickLocs.Count < 2*chainCount ) {

                var pick = validLocs[ Rand.Next(validLocs.Count) ];
                pickLocs.Add(pick);

                // remove tiles that are too close
                var newList = validLocs.Where( (w) => { 
                    return !pick.IsClose2(w,30);
                }).ToList();
                validLocs = newList;

                // validate process
                if ( validLocs.Count<1 ) { 
                    throw new SystemException("Mission Builder Failed to Find Chain Points ["+pickLocs.Count+"/"+(2*chainCount)+"]");    
                }
            }

//Console.WriteLine("MISSION TILES REMAIN = "+validLocs.Count);
            return pickLocs;
        }

        internal void ScanMap( Action<Tile,Where> scan ) {
            for (int ix=0;ix<Terrain.Wide;ix++) {
                for (int iy=0;iy<Terrain.Tall;iy++) {
                    scan.Invoke( Terrain.Grid[ix,iy], new Where(ix,iy) );
                }
            }
        }

        /// <summary>
        /// Find the closest point to the first point, then shift that into the second position.
        /// </summary>
        /// <param name="locs"></param>
        internal void MakeFirstPointsClose( List<Where> locs ) {

            var src = locs[0];

            var dist = int.MaxValue;
            var pick = 0;

            for (int ix=1;ix<locs.Count;ix++ ) {
                var dist2 = locs[ix].Distance2(src);
                if (dist2<dist) {
                    dist = dist2;
                    pick = ix;
                }
            }

            // swap points
            var swap = locs[pick];
            locs.RemoveAt(pick);
            locs.Insert( 1, swap );     // position #2
        }

//======================================================================================================================

        internal void AddEvent( Rule rule, Where loc ) {
//Console.WriteLine("ADDING EVENT ==> "+loc.ToDisplay(false)+"  "+rule.ToDisplay());
            // add facility to tile
            var tile = Terrain.GetTile(loc);
            tile.Site = SiteFactory.MakeEventCoreSite( loc, rule );

            // mark adjacent tiles for spout/sink
            var eventType = ( rule.Type==Make ? EventSpout : EventSink );

            foreach ( var step in SiteTypeInfo.DirStep ) {

                var adj = Terrain.Get( loc.Plus(step) );
                if ( adj==null ) continue;
                if ( NoBuildSite(adj.Type) ) continue;

                adj.Site = SiteFactory.Create( eventType );

            }
        }

    }

}
