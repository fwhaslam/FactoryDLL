// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using FactoryModel.Models.Constants;

using static FactoryModel.Models.RuleType;
using static FactoryModel.Models.Constants.FacilityTypeEnum;

namespace FactoryModel.Models {

    public enum RuleType {
        Make,       // product enter system
        Vend,       // products exits system for money
        Edit        // product changes from one to another
    }

    /// <summary>
    /// Rules are Nodes.  Products are the links between them.
    /// Graph chracteristics are described in Market.cs
    /// </summary>
    public class Rule {

        public int Tier { get; set; } = 1;

        public RuleType Type {  get; set; } = Edit;

        public FacilityTypeEnum Facility { get; set; } = Empty;

        public Recipe Ins { get; set; } =  new Recipe();

        public Recipe Out { get; set; } = new Recipe();

        public string ToDisplay(bool header=true) { 
            var info = "Tier="+Tier+",Type="+Type+",Facility="+Facility+"," +
                "Ins=["+Ins.ToDisplay(false)+"],Out=["+Out.ToDisplay(false)+"]";
            if (header) return "Rule("+info+")";
            return info;
        }
    }
}
