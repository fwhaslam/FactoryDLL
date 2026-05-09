// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static FactoryModel.Models.SiteTypeEnum;
using static FactoryModel.Models.SiteTradeEnum;

namespace FactoryModel.Models {

    public enum SiteTypeEnum {
        Ship,
        Town,
        Mine,
        Farm,
        Ranch,
        Seanet,
        Skynet,
    }

    public enum SiteTradeEnum {
        Buy,
        Sell,
        Mixed
    }

    /// <summary>
    /// A location/region in the game which produces or consumes some product.
    /// </summary>
    public class Site {

        public string Name { get; set; } = "Default";

        public SiteTypeEnum Type { get; set; } = Ship;

        public SiteTradeEnum Trade { get; set; } = Buy;

        /// <summary>
        /// How many facilities can be attached to this port.
        /// </summary>
        public int Entry { get; set; } = 1;
    }
}
