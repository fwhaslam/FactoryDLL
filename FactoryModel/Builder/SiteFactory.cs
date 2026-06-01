// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models;
using FactoryModel.Models.Constants;
using FactoryModel.Models.Sites;

using static FactoryModel.Models.Constants.SiteType;

namespace FactoryModel.Builder {

    /// <summary>
    /// Create Site objects based on the SiteType.
    /// </summary>
    public class SiteFactory {


        public static Site Create( SiteType type ) {
//Console.WriteLine("Creating Site = "+type);

            if (type==None || type==EventSpout || type==EventSink) {
                return MakeEmptySite( type );
            };


            // default, couldn't figure it out.
            throw new SystemException("COuld not build Site for type = "+type );
        }

        public static Site MakeEmptySite( SiteType type = None ) {
            return new EmptySite() { Type = type };
        }

        public static Site MakeBeltSite( SiteType type ) {
            return new BeltSite() { Type = type };
        }


        static public Site MakeEventCoreSite( Where loc, Rule rule ) {
            return new BigSite() {
                ULC = loc,
                Core = new EventSite() {
                    Rule = rule,
                    Type = EventCore
                },
                Tiles = new List<Where>() { loc }
            };
        }
    }
}
