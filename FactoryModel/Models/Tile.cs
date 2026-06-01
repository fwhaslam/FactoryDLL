// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models.Constants;
using FactoryModel.Models.Sites;

namespace FactoryModel.Models {

    public class Tile {

        static public Tile DEFAULT = new Tile();

        public Tile() { }

        public Tile( int X, int Y ) {
            Loc = new Where(X,Y);
        }

        public Tile( Where loc ) {
            Loc = loc;
        }

        public Tile( Where loc, Sites.Site facility ) {
            Loc = loc;
            Site = facility;
        }

        public Where Loc { get; set; } = Where.NOWHERE;

        public TileType Type { get; set; } = TileType.Plains;

        public int Height { get; set; } = 0;

        public Sites.Site Site { get; set; } = new EmptySite();

    }
}
