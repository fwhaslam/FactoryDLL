// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FactoryModel.Models.Constants.FacilityTypeEnum;

namespace FactoryModel.Models.Constants {

    public enum FacilityTypeEnum {

        Empty,

        // 1 in, 1 out ( 12 total )
        BeltW2E,BeltE2N,BeltW2N,BeltS2N,BeltS2W,BeltS2E,
        BeltE2S,BeltN2W,BeltW2S,BeltE2W,BeltN2S,BeltN2E,

        // 1 in, 2 out ( 12 total )
        BeltN2SW,BeltE2NS,BeltS2NE,BeltS2NW,BeltN2SE,BeltN2EW,
        BeltE2NW,BeltW2NE,BeltS2EW,BeltE2SW,BeltW2SE,BeltW2NS,

        // 1 in, 3 out ( 4 total )
        BeltW2NSE,BeltE2NSW,BeltS2NEW,BeltN2SEW,
        
        // 2 in, 1 out ( 12 total )
        BeltNE2S,BeltNS2W,BeltNW2S,BeltNS2E,BeltNE2W,BeltNW2E,
        BeltSW2N,BeltSE2N,BeltEW2N,BeltSE2W,BeltSW2E,BeltEW2S,

        // 2 in, 2 out ( 6 total )
        BeltSE2NW,BeltSW2NE,BeltEW2NS,BeltNW2SE,BeltNE2SW,BeltNS2EW,

        // 3 in, 1 out ( 4 total )
        BeltNSW2E,BeltNEW2S,BeltSEW2N,BeltNSE2W,

        // crossover
        CrossN2SxE2W, CrossS2NxE2W, CrossN2SxW2E, CrossS2NxW2E,
        
        // 1x1 single input processor
        CrusherE2W,CrusherN2S, CrusherW2E, CrusherS2N,

        // basic facilities which are mapped to EditTypes
        EditOneVOne, EditOneVTwo, 
        EditTwoVOne, EditTwoVTwo,
        EditThreeVOne, EditThreeVTwo, EditThreeVThree, 
        EditFiveVOne, EditFiveVThree, EditEightVOne, EditEightVThree,

        // specific facility types
        Haul,       // remove trash, or lowest tier product extractor
        Dump        // drop trash, or lowest tier product vendor
    }

    public static class FacilityTypeEnumInfo {

        public static FacilityTypeEnum DEFAULT_BELT_ENUM = BeltS2N;

        // for IsBelt() range of values
        public static FacilityTypeEnum BEFORE_BELTS = Empty;
        public static FacilityTypeEnum AFTER_BELTS = CrossN2SxE2W;
    }

}
