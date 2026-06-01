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

using static FactoryModel.Models.Constants.TileType;
using static FactoryModel.Models.Constants.SiteType;
using FactoryModel.Models.Sites;
using FactoryModel.Models.Constants;


namespace FactoryModelTests.Builder {


    [TestClass]
    public class TerrainManagerTest {

        //internal static FacilityTypeEnum GetBeltType( Facility facility ) {
        //   return facility.Type;
        //}

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

        internal BeltSiteManager SimpleManager() {
            return new BeltSiteManager() { Map = SimpleMap() };
        }

        [TestMethod]
        public void StartChange_SubmitToPlan_CancelChange() { 

            var mgr = SimpleManager();

            var loc1 = new Where(2,2);

            // invoke
            mgr.StartChange();
            AreEqual( 0, mgr.Plan.Count );

            // invoke
            var results = mgr.SubmitBeltToPlan( loc1 );

            // assertions
            AreEqual( 1, mgr.Plan.Count );
            IsNotNull(mgr.Plan[loc1]);
            AreEqual( 1, results.Count );
            AreEqual( loc1, results[0] );
            
            AreEqual( 1, mgr.Plan.Count );
            AreEqual( BeltS2N, mgr.Plan[loc1].Site.Type );

            // invoke
            mgr.CancelChange();
            AreEqual( 0, mgr.Plan.Count );

        }

        [TestMethod]
        public void AddTwoInARow() { 

            var mgr = SimpleManager();
            mgr.StartChange();

            var loc1 = new Where(2,2);
            var loc2 = new Where(3,2);  // step East

            // invoke
            mgr.SubmitBeltToPlan( loc1 );
            var results = mgr.SubmitBeltToPlan( loc2 );

            // asertions
            AreEqual( 2, results.Count );
            AreEqual( loc2, results[0] );
            AreEqual( loc1, results[1] );

            AreEqual( 2, mgr.Plan.Count );
            AreEqual( BeltW2E, mgr.Plan[loc1].Site.Type);
            AreEqual( BeltW2E, mgr.Plan[loc2].Site.Type);
        }

        [TestMethod]
        public void AddThreeInARow() { 

            var mgr = SimpleManager();
            mgr.StartChange();

            var loc1 = new Where(1,2);
            var loc2 = new Where(2,2);  // step East
            var loc3 = new Where(3,2);  // step East

            // invoke
            mgr.SubmitBeltToPlan( loc1 );
            mgr.SubmitBeltToPlan( loc2 );
            var results = mgr.SubmitBeltToPlan( loc3 );

            // asertions
            AreEqual( 2, results.Count );
            AreEqual( loc3, results[0] );
            AreEqual( loc2, results[1] );

            AreEqual( 3, mgr.Plan.Count );
            AreEqual( BeltW2E, mgr.Plan[loc1].Site.Type);
            AreEqual( BeltW2E, mgr.Plan[loc2].Site.Type);
            AreEqual( BeltW2E, mgr.Plan[loc3].Site.Type);
        }

        [TestMethod]
        public void AddThreeInALoop() { 

            var mgr = SimpleManager();
            mgr.StartChange();

            var loc1 = new Where(2,2);
            var loc2 = new Where(3,2);  // step East
            var loc3 = new Where(3,3);  // step North

            // invoke
            mgr.SubmitBeltToPlan( loc1 );
            mgr.SubmitBeltToPlan( loc2 );
            var results = mgr.SubmitBeltToPlan( loc3 );

            // asertions
            AreEqual( 2, results.Count );
            AreEqual( loc3, results[0] );
            AreEqual( loc2, results[1] );

            AreEqual( 3, mgr.Plan.Count );
            AreEqual( BeltW2E, mgr.Plan[loc1].Site.Type);
            AreEqual( BeltW2N, mgr.Plan[loc2].Site.Type);
            AreEqual( BeltS2N, mgr.Plan[loc3].Site.Type);
        }

        [TestMethod]
        public void AddFourInALoop() { 

            var mgr = SimpleManager();
            mgr.StartChange();

            var loc1 = new Where(2,2);
            var loc2 = new Where(3,2);  // step East
            var loc3 = new Where(3,3);  // step North
            var loc4 = new Where(2,3);  // step West

            // invoke
            mgr.SubmitBeltToPlan( loc1 );
            mgr.SubmitBeltToPlan( loc2 );
            mgr.SubmitBeltToPlan( loc3 );
            var results = mgr.SubmitBeltToPlan( loc4 );

            // asertions
            AreEqual( 2, results.Count );
            AreEqual( loc4, results[0] );
            AreEqual( loc3, results[1] );

            AreEqual( 4, mgr.Plan.Count );
            AreEqual( BeltW2E, mgr.Plan[loc1].Site.Type);
            AreEqual( BeltW2N, mgr.Plan[loc2].Site.Type);
            AreEqual( BeltS2W, mgr.Plan[loc3].Site.Type);   // E2W
            AreEqual( BeltE2W, mgr.Plan[loc4].Site.Type);   // E2W
        }
    }
}
