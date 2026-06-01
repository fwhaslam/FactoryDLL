// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models.Constants;
using FactoryModel.Models.Sites;
using static FactoryModel.Models.Constants.LinkType;

namespace FactoryModel.Models {

    /// <summary>
    /// Directional information about linking facilities together.
    /// This is mostly to record 'open' links to make place belts more logical.
    /// Tiles may have a maximum of four links, one for each ordinal direction ( DirectionTypeEnum ).
    /// Facilities may have more links if they are bigger than a single tile.
    /// </summary>
    public class Link {

        public LinkType Type { get; set; } = NO_LINK;

        public Sites.Site Facility { get; set; } = null;
    }
}
