// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System.Collections.Generic;

using static FactoryModel.Models.Constants.SiteType;
using static FactoryModel.Models.Constants.BeltDisplayType;
using FactoryModel.Tools;

using static FactoryModel.Tools.SystemTools;

namespace FactoryModel.Models.Constants {
    
    /// <summary>
    /// Base belt display types.  
    /// Belt facilities map to these with rotation and animation reversal.
    /// </summary>
    public enum BeltDisplayType {
        OneInOutThru,       // orient: W2E
        OneInOutRight,      // orient: W2S
        OneInTwoOutTee,     // TwoInOneOutTee   // orient: W2NS
        OneInTwoOutRight,   // TwoInOneOutXXX   // orient: W2SE
        OneInTwoOutLeft,    // TwoInOneOutXXX   // orient: W2NE
        OneInThreeOut,      // ThreeInOneOut        // orient: W2NSE
        TwoInOutThru,       // orient: NE2SW
        TwoInOutBend,       // orient: EW2NS
        Crossover           // orient: EN2WS
    }

    public class BeltSiteInfo : KeyedElementIf<SiteType> {

        public BeltSiteInfo( SiteType key, BeltDisplayType _base, int spin, bool reverse ) {
            Key = key;
            Base = _base;
            Spin = spin;
            Reverse = reverse;
        }

        public SiteType Key {  get; set; }
        
        public BeltDisplayType Base { get; set; }

        /// <summary>
        /// How many 90 degree turns clockwise to bet 'base' into position.
        /// </summary>
        public int Spin { get; set; }

        /// <summary>
        /// Wether or not to reverse the belt animation.
        /// </summary>
        public bool Reverse { get; set; }


//======================================================================================================================

        internal static readonly List<BeltSiteInfo> BeltDisplayList = new List<BeltSiteInfo>() {

            // 1 in, 1 out ( 12 total )
            new BeltSiteInfo(BeltE2W,OneInOutThru,2,false),
            new BeltSiteInfo(BeltW2E,OneInOutThru,0,false),
            new BeltSiteInfo(BeltN2S,OneInOutThru,1,false),
            new BeltSiteInfo(BeltS2N,OneInOutThru,3,false),

            new BeltSiteInfo(BeltN2W,OneInOutRight,1,false),
            new BeltSiteInfo(BeltE2N,OneInOutRight,2,false),
            new BeltSiteInfo(BeltS2E,OneInOutRight,3,false),
            new BeltSiteInfo(BeltW2S,OneInOutRight,0,false),

            new BeltSiteInfo(BeltW2N,OneInOutRight,1,true),
            new BeltSiteInfo(BeltN2E,OneInOutRight,2,true),
            new BeltSiteInfo(BeltE2S,OneInOutRight,3,true),
            new BeltSiteInfo(BeltS2W,OneInOutRight,0,true),

            // 1 in, 2 out ( 12 total )
            new BeltSiteInfo(BeltW2NS,OneInTwoOutTee,0,false),
            new BeltSiteInfo(BeltN2EW,OneInTwoOutTee,1,false),
            new BeltSiteInfo(BeltE2NS,OneInTwoOutTee,2,false),
            new BeltSiteInfo(BeltS2EW,OneInTwoOutTee,3,false),

            new BeltSiteInfo(BeltW2NE,OneInTwoOutLeft,0,false),
            new BeltSiteInfo(BeltN2SE,OneInTwoOutLeft,1,false),
            new BeltSiteInfo(BeltE2SW,OneInTwoOutLeft,2,false),
            new BeltSiteInfo(BeltS2NW,OneInTwoOutLeft,3,false),

            new BeltSiteInfo(BeltW2SE,OneInTwoOutRight,0,false),
            new BeltSiteInfo(BeltN2SW,OneInTwoOutRight,1,false),
            new BeltSiteInfo(BeltE2NW,OneInTwoOutRight,2,false),
            new BeltSiteInfo(BeltS2NE,OneInTwoOutRight,3,false),

            // 1 in, 3 out ( 4 total )
            new BeltSiteInfo(BeltW2NSE,OneInThreeOut,0,false),
            new BeltSiteInfo(BeltN2SEW,OneInThreeOut,1,false),
            new BeltSiteInfo(BeltE2NSW,OneInThreeOut,2,false),
            new BeltSiteInfo(BeltS2NEW,OneInThreeOut,3,false),

            // 2 in, 1 out ( 12 total )
            new BeltSiteInfo(BeltNS2W,OneInTwoOutTee,0,true),
            new BeltSiteInfo(BeltEW2N,OneInTwoOutTee,1,true),
            new BeltSiteInfo(BeltNS2E,OneInTwoOutTee,2,true),
            new BeltSiteInfo(BeltEW2S,OneInTwoOutTee,3,true),

            new BeltSiteInfo(BeltNE2W,OneInTwoOutLeft,0,true),
            new BeltSiteInfo(BeltSE2N,OneInTwoOutLeft,1,true),
            new BeltSiteInfo(BeltSW2E,OneInTwoOutLeft,2,true),
            new BeltSiteInfo(BeltNW2S,OneInTwoOutLeft,3,true),

            new BeltSiteInfo(BeltSE2W,OneInTwoOutRight,0,true),
            new BeltSiteInfo(BeltSW2N,OneInTwoOutRight,1,true),
            new BeltSiteInfo(BeltNW2E,OneInTwoOutRight,2,true),
            new BeltSiteInfo(BeltNE2S,OneInTwoOutRight,3,true),

            // 2 in, 2 out ( 6 total )
            new BeltSiteInfo(BeltNW2SE,TwoInOutThru,1,false),
            new BeltSiteInfo(BeltNE2SW,TwoInOutThru,0,false),
            new BeltSiteInfo(BeltSE2NW,TwoInOutThru,3,false),
            new BeltSiteInfo(BeltSW2NE,TwoInOutThru,2,false),

            new BeltSiteInfo(BeltNS2EW,TwoInOutBend,1,false),
            new BeltSiteInfo(BeltEW2NS,TwoInOutBend,0,false),

            // 3 in, 1 out ( 4 total )
            new BeltSiteInfo(BeltNSE2W,OneInThreeOut,0,true),
            new BeltSiteInfo(BeltSEW2N,OneInThreeOut,3,true),
            new BeltSiteInfo(BeltNSW2E,OneInThreeOut,2,true),
            new BeltSiteInfo(BeltNEW2S,OneInThreeOut,1,true),

            // 2 in, 2 out ( crossover )
            new BeltSiteInfo(CrossN2SxE2W,Crossover,0,false),
            new BeltSiteInfo(CrossS2NxE2W,Crossover,0,false),
            new BeltSiteInfo(CrossN2SxW2E,Crossover,0,false),
            new BeltSiteInfo(CrossS2NxW2E,Crossover,0,false),


        };

        public static readonly IDictionary<SiteType,BeltSiteInfo> BeltDisplayMap = 
            AsKeyedDictionary<SiteType,BeltSiteInfo>( BeltDisplayList );
    }

}

