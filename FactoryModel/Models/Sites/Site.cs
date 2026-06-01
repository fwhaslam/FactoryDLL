// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FactoryModel.Models.Constants;

using static FactoryModel.Models.Constants.SiteGroup;
using static FactoryModel.Models.Constants.LinkType;

namespace FactoryModel.Models.Sites {

    /// <summary>
    /// Also defaults as EmptyFacility.
    /// </summary>
    abstract public class Site {

        abstract public SiteGroup Group { get; }

        virtual public SiteType Type { get; set; } 

        virtual public Dictionary<Edge,LinkType> LinkMap { get; set; }

        virtual public LinkType GetLink( Edge edge ) {
            if (!LinkMap.ContainsKey(edge)) return NO_LINK;
            return LinkMap[edge];
        }

        /// <summary>
        /// Root class for factory processing.
        /// </summary>
        virtual public void Process() {  }
    }
}
