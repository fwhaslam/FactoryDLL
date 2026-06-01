// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FactoryModel.Models.Constants;

using static FactoryModel.Models.Constants.SiteGroup;

namespace FactoryModel.Models.Sites {

    /// <summary>
    /// Location of map Event for making or vending products.
    /// </summary>
    public class EventSite : Site {

        override public SiteGroup Group {  get {  return SiteGroup.Event; } }

        public Rule Rule { get; set; }

    }
}
