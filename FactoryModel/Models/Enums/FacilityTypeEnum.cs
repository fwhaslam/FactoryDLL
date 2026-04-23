// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FactoryModel.Models.Enums.FacilityTypeEnum;

namespace FactoryModel.Models.Enums {

    public enum FacilityTypeEnum {

        Empty,

        // 1 in, 1 out ( 12 total )
        BeltE2W,BeltW2N,BeltE2N,BeltN2E,BeltN2W,BeltS2N,
        BeltE2S,BeltN2S,BeltS2E,BeltS2W,BeltW2E,BeltW2S,

        // 1 in, 2 out ( 12 total )
        BeltS2NE,BeltN2SE,BeltW2NS,BeltW2NE,BeltE2NW,BeltE2NS,
        BeltS2NW,BeltN2SW,BeltS2EW,BeltE2SW,BeltN2EW,BeltW2SE,

        // 1 in, 3 out ( 4 total )
        BeltW2NSE,BeltS2NEW,BeltE2NSW,BeltN2SEW,
        
        // 2 in, 1 out ( 12 total )
        BeltSE2W,BeltSW2E,BeltSE2N,BeltEW2S,BeltSW2N,BeltNS2E,
        BeltNE2W,BeltNW2E,BeltNS2W,BeltNE2S,BeltNW2S,BeltEW2N,
        
        // 2 in, 2 out ( 6 total )
        BeltNW2SE,BeltNE2SW,BeltSE2NW,BeltSW2NE,BeltNS2EW,BeltEW2NS,
        
        // 3 in, 1 out ( 4 total )
        BeltNEW2S,BeltSEW2N,BeltNSE2W,BeltNSW2E,

        //// crossover
        CrossN2SxE2W, CrossS2NxE2W, CrossN2SxW2E, CrossS2NxW2E,
        
        // 1x1 single input processor
        CrusherE2W,CrusherN2S, CrusherW2E, CrusherS2N,
    }

}
