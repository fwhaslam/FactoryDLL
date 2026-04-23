// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models;

namespace FactoryModelTests.Models {

    [TestClass]
    public class WhereTest {

        public void _constructor() {
            // invoke
            var work = new Where( 3, 5 );

            // assertions
            AreEqual( 3, work.X );
            AreEqual( 5, work.Y );
        }

        public void Plus() {
            var first = new Where( 3, 5 );
            var second = new Where( -1, -2 );

            // invoke
            var work = first.Plus( second );

            // assertions
            AreEqual( 2, work.X );
            AreEqual( 3, work.Y );
        }
        
        public void Minus() {
            var first = new Where( 3, 5 );
            var second = new Where( -1, -2 );

            // invoke
            var work = first.Minus( second );

            // assertions
            AreEqual( 4, work.X );
            AreEqual( 7, work.Y );
        }
    }
}
