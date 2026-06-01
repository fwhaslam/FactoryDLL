// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using static VerboseCSharp.Asserts.StringAsserts;

using System;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

using FactoryModel.Builder;
using FactoryModel.Tools;
using System.Collections.Generic;
using FactoryModel.Models.Constants;

namespace FactoryModelTests.Builder {

    [TestClass]
    public class MissionBuilderTest {

         [TestMethod]
        public void Build() {

            var builder = new MissionBuilder() { 
                Rand = new RandTool(12345)
            };

            //invoke
            builder.Build( new MissionConfig() {
                Size = 32
            });

            // assertions
            Console.WriteLine( "Mission="+builder.Mission.ToDisplay() );

            // scan for facilities
            var found = new Dictionary<SiteType,int>();
            builder.Terrain.Scan( (t) => {
                var type = t.Site.Type;
                if (!found.ContainsKey(type)) found[type] = 0;
                found[type]++;
            });

            var foundDisplay =  found.Aggregate( "", (a,e) => {
                return a + "/" + e.Key+"="+e.Value ;
            });
            Console.WriteLine( "Sites=" + foundDisplay );

        }

    }

}
