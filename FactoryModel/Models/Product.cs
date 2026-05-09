// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel.Models {

    /// <summary>
    /// An object type produced and consumed within the economy.
    /// </summary>
    public class Product {

        public string Name { get; set; } = "Default";

        public int Value { get; set; } = 1;

        public string ToDisplay( bool header = true ) {
            var display = Name+"@" + Value;
            if (!header) return display;
            return "Product("+display+")";
        }

    }
}
