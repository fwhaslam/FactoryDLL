// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System.Collections.Generic;

using static FactoryModel.Models.Constants.FacilityTypeEnum;
using static FactoryModel.Models.Constants.BeltDisplayBaseEnum;
using FactoryModel.Tools;

using static FactoryModel.Tools.SystemTools;

namespace FactoryModel.Models.Constants {
    
    /// <summary>
    /// Base belt display types.  
    /// Belt facilities map to these with rotation and animation reversal.
    /// </summary>
    public enum BeltDisplayBaseEnum {
        OneInOutThru,   // orient: W2E
        OneInOutRight,   // orient: W2S
        OneInTwoOutTee, // TwoInOneOutTee   // orient: W2NS
        OneInTwoOutRight,   // TwoInOneOutXXX   // orient: W2SE
        OneInTwoOutLeft,    // TwoInOneOutXXX   // orient: W2NE
        OneInThreeOut,  // ThreeInOneOut        // orient: W2NSE
        TwoInOutThru,   // orient: NE2SW
        TwoInOutBend    // orient: EW2NS
    }

    public class BeltDisplayInfo : KeyedElementIf<FacilityTypeEnum> {

        public BeltDisplayInfo( FacilityTypeEnum key, BeltDisplayBaseEnum _base, int spin, bool reverse ) {
            Key = key;
            Base = _base;
            Spin = spin;
            Reverse = reverse;
        }

        public FacilityTypeEnum Key {  get; set; }
        
        public BeltDisplayBaseEnum Base { get; set; }

        /// <summary>
        /// How many 90 degree turns clockwise to bet 'base' into position.
        /// </summary>
        public int Spin { get; set; }

        /// <summary>
        /// Wether or not to reverse the belt animation.
        /// </summary>
        public bool Reverse { get; set; }

        internal static readonly List<BeltDisplayInfo> BeltDisplayList = new List<BeltDisplayInfo>() {

            // 1 in, 1 out ( 12 total )
            new BeltDisplayInfo(BeltE2W,OneInOutThru,2,false),
            new BeltDisplayInfo(BeltW2E,OneInOutThru,0,false),
            new BeltDisplayInfo(BeltN2S,OneInOutThru,1,false),
            new BeltDisplayInfo(BeltS2N,OneInOutThru,3,false),

            new BeltDisplayInfo(BeltN2W,OneInOutRight,1,false),
            new BeltDisplayInfo(BeltE2N,OneInOutRight,2,false),
            new BeltDisplayInfo(BeltS2E,OneInOutRight,3,false),
            new BeltDisplayInfo(BeltW2S,OneInOutRight,0,false),

            new BeltDisplayInfo(BeltW2N,OneInOutRight,1,true),
            new BeltDisplayInfo(BeltN2E,OneInOutRight,2,true),
            new BeltDisplayInfo(BeltE2S,OneInOutRight,3,true),
            new BeltDisplayInfo(BeltS2W,OneInOutRight,0,true),

            // 1 in, 2 out ( 12 total )
            new BeltDisplayInfo(BeltW2NS,OneInTwoOutTee,0,false),
            new BeltDisplayInfo(BeltN2EW,OneInTwoOutTee,1,false),
            new BeltDisplayInfo(BeltE2NS,OneInTwoOutTee,2,false),
            new BeltDisplayInfo(BeltS2EW,OneInTwoOutTee,3,false),

            new BeltDisplayInfo(BeltW2NE,OneInTwoOutLeft,0,false),
            new BeltDisplayInfo(BeltN2SE,OneInTwoOutLeft,1,false),
            new BeltDisplayInfo(BeltE2SW,OneInTwoOutLeft,2,false),
            new BeltDisplayInfo(BeltS2NW,OneInTwoOutLeft,3,false),

            new BeltDisplayInfo(BeltW2SE,OneInTwoOutRight,0,false),
            new BeltDisplayInfo(BeltN2SW,OneInTwoOutRight,1,false),
            new BeltDisplayInfo(BeltE2NW,OneInTwoOutRight,2,false),
            new BeltDisplayInfo(BeltS2NE,OneInTwoOutRight,3,false),

            // 1 in, 3 out ( 4 total )
            new BeltDisplayInfo(BeltW2NSE,OneInThreeOut,0,false),
            new BeltDisplayInfo(BeltN2SEW,OneInThreeOut,1,false),
            new BeltDisplayInfo(BeltE2NSW,OneInThreeOut,2,false),
            new BeltDisplayInfo(BeltS2NEW,OneInThreeOut,3,false),

            // 2 in, 1 out ( 12 total )
            new BeltDisplayInfo(BeltNS2W,OneInTwoOutTee,0,true),
            new BeltDisplayInfo(BeltEW2N,OneInTwoOutTee,1,true),
            new BeltDisplayInfo(BeltNS2E,OneInTwoOutTee,2,true),
            new BeltDisplayInfo(BeltEW2S,OneInTwoOutTee,3,true),

            new BeltDisplayInfo(BeltNE2W,OneInTwoOutLeft,0,true),
            new BeltDisplayInfo(BeltSE2N,OneInTwoOutLeft,1,true),
            new BeltDisplayInfo(BeltSW2E,OneInTwoOutLeft,2,true),
            new BeltDisplayInfo(BeltNW2S,OneInTwoOutLeft,3,true),

            new BeltDisplayInfo(BeltSE2W,OneInTwoOutRight,0,true),
            new BeltDisplayInfo(BeltSW2N,OneInTwoOutRight,1,true),
            new BeltDisplayInfo(BeltNW2E,OneInTwoOutRight,2,true),
            new BeltDisplayInfo(BeltNE2S,OneInTwoOutRight,3,true),

            // 2 in, 2 out ( 6 total )
            new BeltDisplayInfo(BeltNW2SE,TwoInOutThru,1,false),
            new BeltDisplayInfo(BeltNE2SW,TwoInOutThru,0,false),
            new BeltDisplayInfo(BeltSE2NW,TwoInOutThru,3,false),
            new BeltDisplayInfo(BeltSW2NE,TwoInOutThru,2,false),

            new BeltDisplayInfo(BeltNS2EW,TwoInOutBend,1,false),
            new BeltDisplayInfo(BeltEW2NS,TwoInOutBend,0,false),

            // 3 in, 1 out ( 4 total )
            new BeltDisplayInfo(BeltNSE2W,OneInThreeOut,0,true),
            new BeltDisplayInfo(BeltSEW2N,OneInThreeOut,3,true),
            new BeltDisplayInfo(BeltNSW2E,OneInThreeOut,2,true),
            new BeltDisplayInfo(BeltNEW2S,OneInThreeOut,1,true),

        };

        public static readonly IDictionary<FacilityTypeEnum,BeltDisplayInfo> BeltDisplayMap = 
            AsKeyedDictionary<FacilityTypeEnum,BeltDisplayInfo>( BeltDisplayList );
    }

}

