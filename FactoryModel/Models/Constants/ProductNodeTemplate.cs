// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel.Models.Constants {

    /// <summary>
    /// Product transformation node template.
    /// </summary>
    public class ProductNodeTemplate {

        public static readonly List<ProductNodeTemplate> NodeTemplates = new List<ProductNodeTemplate>() {
            new ProductNodeTemplate("Farmer",10,0,1),
            new ProductNodeTemplate("Rancher",10,0,1),
            new ProductNodeTemplate("Miner",20,0,1),
            new ProductNodeTemplate("Vendor",10,1,0),

            new ProductNodeTemplate("Smasher",10,1,1),
            new ProductNodeTemplate("Merger",50,2,1),
            new ProductNodeTemplate("Splitter",100,1,3),

        };


        public ProductNodeTemplate( string n, int c, int i, int o ) {
            Name = n;
            Cost = c;
            Ins = i;
            Out = o;
        }
        public string Name { get; set; }

        public int Cost { get; set; }

        public int Ins { get; set; }

        public int Out { get; set; }
    }
}
