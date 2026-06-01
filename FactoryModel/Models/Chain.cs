// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;

namespace FactoryModel.Models {

    /// <summary>
    /// A sequence of rules from Make to Vend.
    /// </summary>
    public class Chain : List<Rule> {

        public int Tier { get { return FirstRule.Tier; } }

        public Rule FirstRule { get {  return this[0]; } }

        public Rule LastRule { get {  return this[ Count-1 ]; } }
    }
}
