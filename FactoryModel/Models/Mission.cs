// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Builder;

namespace FactoryModel.Models {

    public class Mission {

        public MissionConfig Config { get; set; }

        public Terrain Terrain {  get; set; }

        public Market Market {  get; set; }

        public List<Event> Events { get; set; }

        public string ToDisplay() {
            return Market.ToDisplay();
        }
    }
}
