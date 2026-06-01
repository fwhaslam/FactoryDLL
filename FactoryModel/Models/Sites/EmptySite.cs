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
    /// Nothing is built here, but the SiteType may be displaying markers on the map.
    /// 
    /// </summary>
    public class EmptySite : Site {

        override public SiteGroup Group { get {  return Empty; } }

    }
}
