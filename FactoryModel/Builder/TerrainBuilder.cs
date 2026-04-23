// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models;
using FactoryModel.Models.Enums;
using static FactoryModel.Models.Enums.TileTypeEnum;

namespace FactoryModel.Builder {

    public class TerrainBuilder {

        // multiply edge distance, helps to 'cut corners'
        internal static readonly float EDGE_FACTOR = 30f;

        // multiple of base height, to create more interesting bumps.
        internal static readonly int HEIGHT_MULT = 3;

        public Random Rand { get; set; } = new Random( Environment.TickCount );

        public int Size {  get; set; }

        public Terrain Map { get; set; }

        public void Build() { 

            BuildBaseTiles( Size, Size, Plains );

            LayDownSomeRandomSplashes();

            CutTheCorners();

            SwapTileSmoothing();

            FixHeights();

            FixAdjacentHeights();

        }

        internal void BuildBaseTiles( int wide,int tall, TileTypeEnum type ) { 

            Map = new Terrain() {
                Wide = Size,
                Tall = Size,
                Grid = new Tile[Size,Size]
            };
 
            // fill with plains
            var height = TileTypeInfo.InfoMap[Plains].BaseHeight;
            for (int ix=0;ix<Size;ix++) {
                for (int iy=0;iy<Size;iy++) {
                    Map.Grid[ix,iy] = new Tile(ix,iy){
                        Type = Plains,
                        Height = height
                    };
                }
            }
        }

        internal void LayDownSomeRandomSplashes() { 

            var start = Size / 3;

            for ( int radius=start;radius>1;radius--) {

                var type = Rand.Next(0,TileTypeInfo.TileTypeEnumCount);
                SplashColor( (TileTypeEnum)type, radius );
            }

        }


        internal void CutTheCorners() {

            int wide = Map.Wide;
            int tall = Map.Tall;

            var cutType = Sea;
            var cutHeight = TileTypeInfo.InfoMap[Sea].BaseHeight;

            for (int ix=0;ix<wide;ix++) {
                float xdist = Math.Min( ( ix+1f )/wide, (wide-ix)/(float)wide );

                for (int iy=0;iy<Map.Tall;iy++) {
                    float ydist = Math.Min( ( iy+1f )/tall, (tall-iy)/(float)tall );

                    float edgeDist = xdist * ydist;
                    float edgeLimit = edgeDist * EDGE_FACTOR;
//Console.WriteLine( "AT "+ix+" / "+iy+" = "+xdist+" / "+ydist+" = "+edgeDist );
//Console.WriteLine( $"AT {ix:f3} / {iy:f3} = {xdist:f3} / {ydist:f3} = {edgeDist:f3} >> {edgeLimit:f3}" );

                    // change edges towards sea
                    if ( Rand.NextDouble() > edgeLimit ) { 

                        var tile = Map.Grid[ix,iy];
                        tile.Type = cutType;
                        tile.Height = cutHeight;                        
                    }
                }
            }
        }


        internal void SplashColor( TileTypeEnum type, int radius ) {

            var height = TileTypeInfo.InfoMap[type].BaseHeight;

            var cx = Rand.Next( Size );
            var cy = Rand.Next( Size );

            var limit = Math.Pow( radius, 2f );

    //print("CX="+cx+" CY="+cy+" Rad="+radius+" Color="+color);

            for (int ix=cx-radius;ix<=cx+radius;ix++) {
                if (ix<0 || ix>=Size) continue;

                var xdist2 = Math.Pow( ix-cx, 2f );

                for (int iy=cy-radius;iy<=cy+radius;iy++){
                    if (iy<0 || iy>=Size) continue;

                    var dist2 = xdist2 + Math.Pow( iy-cy, 2f );
                    if (dist2>limit) continue;

                    // show more towards center
                    if ( Rand.NextDouble() < dist2/limit ) continue;

                    var tile = Map.Grid[ix,iy];
                    tile.Type = type;
                    tile.Height = height;
                }
            }
        }

//======================================================================================================================

        internal void FixHeights() {

            for (int ix=0;ix<Map.Wide;ix++) {
                for (int iy=0;iy<Map.Tall;iy++) {
                    var work = Map.Grid[ix,iy];
                    work.Height = TileTypeInfo.InfoMap[ work.Type ].BaseHeight * HEIGHT_MULT;
                }
            }
        }

        /// <summary>
        /// Over multiple passes, reduce height to lowest neighbor +1.
        /// </summary>
        internal void FixAdjacentHeights() {

            //int countdownToDitch = 1;

            var more = true;
            while (more) {

                more = false;
                for (int ix=0;ix<Map.Wide;ix++) {
                    for (int iy=0;iy<Map.Tall;iy++) {

                        // skip sea tiles
                        var work = Map.Grid[ix,iy];
                        if (work.Type==Sea) continue;

                        // is anyone lower?
                        var minHeight = LowestNeighborHeight( work );
                        if (minHeight >= work.Height-1 ) continue;

                        // lower height
                        work.Height--;
                        more = true;
//Console.WriteLine( $"AT {ix:f3} / {iy:f3} Height = {work.Height:#}" );

                    }
                }

                //countdownToDitch--;

                //if (countdownToDitch<0) break;
            }
        }

        internal int LowestNeighborHeight( Tile pick ) {

            var min = pick.Height;

            min = Math.Min( min, GetBuddy( pick, 0, +1 ).Height );
            min = Math.Min( min, GetBuddy( pick, 0, -1 ).Height );
            min = Math.Min( min, GetBuddy( pick, -1, 0 ).Height );
            min = Math.Min( min, GetBuddy( pick, +1, 0 ).Height );

            return min;
        }

//======================================================================================================================

        internal Tile DEFAULT_BUDDY = new Tile() { Type = Sea, Height = int.MaxValue };

        /// <summary>
        /// Randomly examine every tile.  
        /// If it is adjacent to a similar tile, done.
        /// If not, then look for a similar tile two steps away, 
        /// and swap type with the intervening tile.
        /// </summary>
        internal void SwapTileSmoothing() {

            var list = new List<Tile>();
            for (int ix=0;ix<Map.Wide;ix++) {
                for (int iy=0;iy<Map.Tall;iy++) {
                    list.Add(Map.Grid[ix,iy]);
                }
            }

            // randomly access and remove
            while (list.Count()>0) {

                var here = Rand.Next(list.Count());
                var spin = Rand.Next(4);

                var pick = list[ here ];
                var type = pick.Type;
                list.RemoveAt( here );

                // don't swap Sea
                if (type==Sea) continue;

                // look for adjacent
                if (HasTileBuddy( pick )) continue;

                // skip a tile, look for swappable
                var next = FindHopBuddy( pick, spin );
                if (next==DEFAULT_BUDDY) continue;

                // swap tile types
                pick.Type = next.Type;
                next.Type = type;

                // swap list entries so 'next' also gets a chance to swap
                list.Remove( next );
                list.Add( pick );
            }
        }

        internal static readonly (int, int)[] HOP = {
            (0,+1), (0,-1), (+1,0), (-1,0),
            (0,+1), (0,-1), (+1,0), (-1,0),
        };

        internal bool HasTileBuddy( Tile pick ) {
            var type = pick.Type;
            for (int hx=0;hx<4;hx++) { 
                var dir = HOP[hx];
                if (GetBuddy( pick, dir.Item1, dir.Item2 ).Type==type) return true;
            }
            return true;
        }

        internal Tile GetBuddy( Tile pick, int dx, int dy ) {
            var ix = pick.Loc.X+dx;
            if ( ix<0 || ix>=Map.Wide) return DEFAULT_BUDDY;
            var iy = pick.Loc.Y+dy;
            if (iy<0 || iy>=Map.Tall) return DEFAULT_BUDDY;
            return Map.Grid[ix,iy];
        }


        internal Tile FindHopBuddy( Tile pick, int spin ) {
            var type = pick.Type;
            for (int hx=0;hx<4;hx++) {
                var dir = HOP[hx+spin];
                var peek = GetBuddy( pick, 2*dir.Item1, 2*dir.Item2 );
                if (peek.Type==type) {
                    return GetBuddy( pick, dir.Item1, dir.Item2 );
                }
            }
            return DEFAULT_BUDDY;
        }
    }

}
