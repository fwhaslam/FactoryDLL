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

namespace FactoryModelTests.Builder {

    [TestClass]
    public class MarketBulderTest {

         [TestMethod]
        public void Build() {

            var builder = new MarketBuilder() { 
                Rand = new Random(12345)
            };
            builder.Size = 10;

            //invoke
            builder.Build();

            // assertions
            Console.WriteLine( "MARKET="+builder.Market.ToDisplay() );
        }

        [TestMethod]
        public void PickLength() {

            AreEqual( 0, MarketBuilder.PickLength( 1, 0.0f ) );
            AreEqual( 0, MarketBuilder.PickLength( 1, 0.25f ) );
            AreEqual( 1, MarketBuilder.PickLength( 1, 0.52f ) );
            AreEqual( 2, MarketBuilder.PickLength( 1, 0.76f ) );
            AreEqual( 3, MarketBuilder.PickLength( 1, 0.96f ) );
        }
    }

}
