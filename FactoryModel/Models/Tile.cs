// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models.Enums;

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

        public Tile( Where loc, FacilityTypeEnum facility ) {
            Loc = loc;
            Facility = facility;
        }

        public Where Loc { get; set; } = Where.NOWHERE;

        public TileTypeEnum Type { get; set; } = TileTypeEnum.Plains;

        public int Height { get; set; } = 0;

        public FacilityTypeEnum Facility { get; set; } = FacilityTypeEnum.Empty;

    }
}
