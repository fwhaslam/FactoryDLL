// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel.Models {

    /// <summary>
    /// Array of tiles, representing a complete map.
    /// </summary>
    public class Terrain {

        public int Wide { get; set; }

        public int Tall { get; set; }

        public Tile[,] Grid { get; set; }

//======================================================================================================================

        public Tile GetTile(Where loc) {
            return Grid[loc.X,loc.Y];
        }

        public bool InBounds( int x, int y ) {
            return ( x>=0 && y>=0 && x<Wide && y<Tall );
        }

        public bool InBounds( Where loc ) {
            return ( loc.X>=0 && loc.Y>=0 && loc.X<Wide && loc.Y<Tall );
        }

        public Tile Get( int x, int y ) {
            if (!InBounds(x,y)) return Grid[x,y];
            return null;
        }

        public Tile Get( Where loc ) {
            if (InBounds(loc)) return Grid[loc.X,loc.Y];
            return null;
        }

        public void Scan( Action<Tile> action ) { 

            for (int ix=0;ix<Wide;ix++) {
                for (int iy=0;iy<Tall;iy++) {
                    action.Invoke(Grid[ix,iy] );
                }
            }
        }

        public void Scan( Action<int,int,Tile> action ) { 

            for (int ix=0;ix<Wide;ix++) {
                for (int iy=0;iy<Tall;iy++) {
                    action.Invoke( ix, iy, Grid[ix,iy] );
                }
            }
        }
    }
}
