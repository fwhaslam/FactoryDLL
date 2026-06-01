// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Tools;

namespace FactoryModel.Models {

    /// <summary>
    /// A collection of Rules defining how the economy functions.
    /// 
    /// Markets are directed graphs that may contain cycles under certain restrictions.
    /// 
    /// </summary>
    public class Market {

        public string name { get; set; } = "Default";

        public IndexedSet<Rule> Rules { get; set; } = new IndexedSet<Rule>();

        public IndexedSet<Product> Products { get; set; } = new IndexedSet<Product>();

        public List<Chain> Chains { get; set; } = new List<Chain>();

        public void AddChain( Chain chain ) {
            Chains.Add( chain );           
            foreach ( var rule in chain ) {
                AddRule( rule );
            }
        }

        public void AddRule( Rule rule ) {
            Rules.Add( rule );
            foreach ( var item in rule.Ins.Keys ) Products.Add( item );
            foreach ( var item in rule.Out.Keys ) Products.Add( item );
        }

        public string ToDisplay() {
            return Rules.Aggregate( "", (show,rule) => show+"\n"+rule.ToDisplay() );
        }
    }
}
