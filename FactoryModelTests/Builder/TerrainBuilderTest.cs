// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using static VerboseCSharp.Asserts.StringAsserts;

using System;
using System.Linq;

using System.Text;
using FactoryModel.Builder;

using static FactoryModel.Models.Constants.TileTypeEnum;

namespace FactoryModelTests.Builder {

    [TestClass]
    public class TerrainBuilderTest {

        [TestMethod]
        public void Builder() {

            var builder = new TerrainBuilder() {
                Rand = new Random( 12345 )
            };
            builder.Size = 20;

            // invocation
            builder.Build();
            var map = builder.Map;

            // assertions
            Assert.AreEqual( 20, map.Wide );
            Assert.AreEqual( 20, map.Tall );
            Assert.IsNotNull( map.Grid );
        }

        [TestMethod]
        public void Builder_CornersCut_CenterFull() {

            var size = 20;
            var limit = size-1;
            var half = size / 2;

            var builder = new TerrainBuilder() {
                Rand = new Random( 12345 )
            };
            builder.Size = size;

            // invocation
            builder.Build();
            var map = builder.Map;

            // assertions
            AreEqual( Sea, map.Grid[0,0].Type );
            AreEqual( Sea, map.Grid[0,limit].Type );
            AreEqual( Sea, map.Grid[limit,limit].Type );
            AreEqual( Sea, map.Grid[limit,0].Type );

            AreNotEqual( Sea, map.Grid[half,half].Type );

        }
    }
}
