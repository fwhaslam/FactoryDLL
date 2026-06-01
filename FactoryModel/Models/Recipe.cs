// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using FactoryModel.Tools;

namespace FactoryModel.Models {

    /// <summary>
    /// A collection of products and counts.  
    /// The counts are used for rule consumption and producton.
    /// </summary>
    public class Recipe : IndexedDictionary<Product, int > { 
        
        public void Increment( Product what ) {
            Include( what, 1 );
        }
    
        public void Include( Product what, int num ) {
            if (!ContainsKey(what)) this[what] = 0;
            this[what] += num;
        }

        public string ToDisplay(bool header=true) { 
            var info = this.Aggregate( "", (str,entry) => str+","+entry.Key.ToDisplay(false)+":"+entry.Value );
            if (header) return "ProductMap("+info+")";
            return info;
        }
    }
} 
