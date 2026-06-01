// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FactoryModel.Models.Constants.SiteType;

namespace FactoryModel.Models.Constants {

    public enum SiteGroup {
        Empty,
        Belt, 
        Make, 
        Vend,
        Edit,
        Manage,
        Event
    };

    public enum SiteType {

        None,

        // mark an existing facility as transitioning to None
        // Remove,       // -- put an EmptySite into the PlanTile to remove something.

        // management facilities
        Managers,

        // event facilities (spout=make, sink=vend)
        EventCore,EventSpout,EventSink,

        // product creation 1x1
        MakerT1,

        // product destruction 1x1
        VendorT1,

        // edit facilities ( currently maps to EditTypeEnum, but may replace )
        EditOneVOne, EditOneVTwo, 
        EditTwoVOne, EditTwoVTwo,
        EditThreeVOne, EditThreeVTwo, EditThreeVThree, 
        EditFiveVOne, EditFiveVThree, EditEightVOne, EditEightVThree,

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
    }

    //public static class SiteTypeInfo {

    //    public static SiteType DEFAULT_BELT_ENUM = BeltS2N;

    //    public static int MAXIMUM_TIER_LIMIT = 6;

    //}

}
