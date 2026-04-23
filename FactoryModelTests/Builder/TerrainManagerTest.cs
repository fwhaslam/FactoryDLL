// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using static VerboseCSharp.Asserts.StringAsserts;

using System;
using System.Linq;

using System.Text;
using FactoryModel.Builder;
using FactoryModel.Models;

using static FactoryModel.Models.Enums.TileTypeEnum;
using static FactoryModel.Models.Enums.FacilityTypeEnum;


namespace FactoryModelTests.Builder {

    [TestClass]
    public class TerrainManagerTest {

        internal Terrain SimpleMap() {

            var map = new Terrain() {
                Wide = 5,
                Tall = 5,
                Grid = new Tile[5,5]
            };

            for (int ix=0;ix<5;ix++) for (int iy=0;iy<5;iy++) {
                map.Grid[ix,iy] = new Tile() {
                    Loc = new Where(ix,iy)
                };
            }

            return map;
        }

        internal TerrainManager SimpleManager() {
            return new TerrainManager() { Map = SimpleMap() };
        }

        [TestMethod]
        public void StartChange_SubmitToPlan_CancelChange() { 

            var mgr = SimpleManager();

            var loc1 = new Where(2,2);

            // invoke
            mgr.StartChange();
            AreEqual( 0, mgr.Plan.Count );

            // invoke
            var results = mgr.SubmitToPlan( loc1, BeltN2S );
            AreEqual( 1, mgr.Plan.Count );
            IsNotNull(mgr.Plan[loc1]);
            AreEqual( 1, results.Count );
            AreEqual( loc1, results[0] );

            // invoke
            mgr.CancelChange();
            AreEqual( 0, mgr.Plan.Count );

        }

        [TestMethod]
        public void AddTwoForceLink() { 

            var mgr = SimpleManager();
            mgr.StartChange();

            var loc1 = new Where(2,2);
            var loc2 = new Where(3,2);

            // invoke
            var ignored = mgr.SubmitToPlan( loc1, BeltN2S );
            var results = mgr.SubmitToPlan( loc2, BeltN2S );

            // asertions
            AreEqual( 2, mgr.Plan.Count );
            AreEqual( BeltW2E, mgr.Plan[loc1].Facility );
            AreEqual( BeltW2E, mgr.Plan[loc2].Facility );

            AreEqual( 2, results.Count );
            AreEqual( loc2, results[0] );
            AreEqual( loc1, results[1] );
        }

        [TestMethod]
        public void AddThreeInALoop() { 


            var mgr = SimpleManager();
            mgr.StartChange();

            var loc1 = new Where(2,2);
            var loc2 = new Where(3,2);
            var loc3 = new Where(3,3);
            //var loc4 = new Where(2,3);

            // invoke
            mgr.SubmitToPlan( loc1, BeltN2S );
            mgr.SubmitToPlan( loc2, BeltN2S );
            var results = mgr.SubmitToPlan( loc3, BeltN2S );
            //mgr.SubmitToPlan( loc4, BeltN2S );

            // asertions
            AreEqual( 3, mgr.Plan.Count );
            AreEqual( BeltS2E, mgr.Plan[loc1].Facility ); // W2E
            AreEqual( BeltW2S, mgr.Plan[loc2].Facility );   // W2E
            AreEqual( BeltN2W, mgr.Plan[loc3].Facility );   // E2W
            //AreEqual( BeltE2N, mgr.Plan[loc4].Facility );   // E2W

            AreEqual( 2, results.Count );
            AreEqual( loc3, results[0] );
            AreEqual( loc2, results[1] );
        }
    }
}
