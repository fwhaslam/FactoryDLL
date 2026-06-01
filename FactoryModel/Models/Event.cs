// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FactoryModel.Models.Constants;
using FactoryModel.Models.Sites;

namespace FactoryModel.Models {

    /// <summary>
    /// A map location which either produces or consumes some product.
    /// Players will attach a facility to Make or Vend products.
    /// 
    /// Events are paired based on Market/Chains.   
    /// The FIRST pair MUST produce Coin for sales.
    /// 
    /// Events are linked to the first and last rules in chains.  
    /// Paired Make and Vend rules have to linked to different events.
    /// The distance between paired events will increase based on Tier.
    /// Events can be linked to rules from multiple chains.
    /// 
    /// Events can be as follows:
    /// 1) make products
    /// 2) consume products and return Coin
    /// 3) consume products to a threshold, then return a purse of Coin.
    /// 4) consume products at a fixed rate which creates a Happy state.
    /// 5) consume products to a threshold, which opens the Gate to later events.
    /// 
    /// </summary>
    public class Event {

        /// <summary>
        /// Gross description of event.
        /// </summary>
        public EventType Type { get; set; }

        /// <summary>
        /// Linked facility with terrain location.
        /// </summary>
        public Sites.Site Facility { get; set; }

        /// <summary>
        /// Linked rule.  Must be Make or Vend type.
        /// </summary>
        public Rule Rule { get; set; }

        /// <summary>
        /// Is this event Gated by earlier events.
        /// </summary>
        public bool Gated { get; set; }


        /// <summary>
        /// Does this event pay Coin based on Vend of product.
        /// </summary>
        public bool Payout { get; set; }

        /// <summary>
        /// Does this event GATE later events.
        /// </summary>
        public bool Gate { get; set; }
        
        /// <summary>
        /// Count of product Made or Vend.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Threhold where something may change.
        /// </summary>
        public int Threshold { get; set; }
    }
}
