// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using static VerboseCSharp.Asserts.StringAsserts;

using System;
using System.Linq;

using System.Collections.Generic;
using System.Text;

using FactoryModel.Builder;

using FactoryModel.Models.Constants;
using FactoryModel.Models.Sites;

using static FactoryModel.Models.Constants.SiteType;
using static FactoryModel.Models.Constants.SiteTypeInfo;
using static FactoryModel.Models.Constants.LinkType;

namespace FactoryModelTests.Models {

    [TestClass]
    public class FacilityTypeEnumTests : SiteTypeInfo {

        internal class FormInfo {
            internal string Name { get; set; }
            internal int Ins {  get; set; }
            internal int Out { get; set; }
        }

        internal int FormInfoComparison( FormInfo a, FormInfo b) {

            var inDelta = a.Ins - b.Ins;
            var outDelta = a.Out - b.Out;

            return 10 * inDelta + outDelta;
        }

        internal void FixBeltGrid(string[,,,] grid, LinkType nx, LinkType sx, LinkType ex, LinkType wx, string value ) {
            grid[(int)nx,(int)sx,(int)ex,(int)wx] = value;
        }

        [TestMethod]
        public void BeltEnumValues() {

            //// NESW each has 3 values: None=0, In=1, Out=2
            //bool[,,,] forms = new bool[3,3,3,3];

            //// Must have at least one IN, and one OUT 
            //var valid = 0;
            //for (int nx = 0; nx < 3; nx++) {
            //    for (int ex = 0; ex < 3; ex++) {
            //        for (int sx = 0; sx < 3; sx++) {

            //            for (int wx = 0; wx < 3; wx++) {
            //                var iNum = (nx==1?1:0) + (ex==1?1:0) + (sx==1?1:0) + (wx==1?1:0);
            //                var oNum = (nx==2?1:0) + (ex==2?1:0) + (sx==2?1:0) + (wx==2?1:0);

            //                if (iNum>0 && oNum>0) { 
            //                    forms[nx,ex,sx,wx] = true;
            //                    valid++;
            //                }
            //            }
            //        }
            //    }
            //}

            // Producd them as 'Enum' terms
            var forms = new List<FormInfo>();

            var defaultBelt = new string[3,3,3,3];
            for (LinkType nx = 0; nx < LinkTypeEnumCount; nx++) {
                for (LinkType sx = 0; sx < LinkTypeEnumCount; sx++) {
                    for (LinkType ex = 0; ex < LinkTypeEnumCount; ex++) {

                        for (LinkType wx = 0; wx < LinkTypeEnumCount; wx++) {

                            var iNum = (nx==IN_LINK?1:0) + (sx==IN_LINK?1:0) + (ex==IN_LINK?1:0) + (wx==IN_LINK?1:0);
                            var oNum = (nx==OUT_LINK?1:0) + (sx==OUT_LINK?1:0) + (ex==OUT_LINK?1:0) + (wx==OUT_LINK?1:0);

                            var name = "Empty";
                            if (iNum>0 && oNum>0) {

                                name = "Belt";
                                if (nx==IN_LINK) name += "N";
                                if (sx==IN_LINK) name += "S";
                                if (ex==IN_LINK) name += "E";
                                if (wx==IN_LINK) name += "W";

                                name += "2";
                                if (nx==OUT_LINK) name += "N";
                                if (sx==OUT_LINK) name += "S";
                                if (ex==OUT_LINK) name += "E";
                                if (wx==OUT_LINK) name += "W";

                                forms.Add( new FormInfo {
                                    Name = name,
                                    Ins = iNum,
                                    Out = oNum
                                } );
                            }

                            defaultBelt[(int)nx,(int)sx,(int)ex,(int)wx] = name;
                        }
                    }
                }
            }

            // replace 'empty' values with more useful defaults
            // no ins + no outs
            FixBeltGrid( defaultBelt, NO_LINK,NO_LINK,NO_LINK,NO_LINK,  "BeltS2N");   // see: DEFAULT_BELT_ENUM

            // no outs
            FixBeltGrid( defaultBelt, IN_LINK,IN_LINK,IN_LINK,IN_LINK, "BeltS2N");

            FixBeltGrid( defaultBelt, IN_LINK,NO_LINK,NO_LINK,NO_LINK, "BeltN2S");
            FixBeltGrid( defaultBelt, NO_LINK,IN_LINK,NO_LINK,NO_LINK, "BeltS2N");
            FixBeltGrid( defaultBelt, NO_LINK,NO_LINK,IN_LINK,NO_LINK, "BeltE2W");
            FixBeltGrid( defaultBelt, NO_LINK,NO_LINK,NO_LINK,IN_LINK, "BeltW2E");

            FixBeltGrid( defaultBelt, IN_LINK,IN_LINK,NO_LINK,NO_LINK, "BeltNS2E");
            FixBeltGrid( defaultBelt, NO_LINK,NO_LINK,IN_LINK,IN_LINK, "BeltEW2N");

            FixBeltGrid( defaultBelt, NO_LINK,IN_LINK,IN_LINK,NO_LINK, "BeltSE2W");
            FixBeltGrid( defaultBelt, IN_LINK,NO_LINK,NO_LINK,IN_LINK, "BeltNW2E");
            FixBeltGrid( defaultBelt, IN_LINK,NO_LINK,IN_LINK,NO_LINK, "BeltNE2S");
            FixBeltGrid( defaultBelt, NO_LINK,IN_LINK,NO_LINK,IN_LINK, "BeltSW2N");

            FixBeltGrid( defaultBelt, IN_LINK,IN_LINK,IN_LINK,NO_LINK, "BeltNSE2W");
            FixBeltGrid( defaultBelt, NO_LINK,IN_LINK,IN_LINK,IN_LINK, "BeltSEW2N");
            FixBeltGrid( defaultBelt, IN_LINK,NO_LINK,IN_LINK,IN_LINK, "BeltNEW2S");
            FixBeltGrid( defaultBelt, IN_LINK,IN_LINK,NO_LINK,IN_LINK, "BeltNSW2E");

            // no ins
            FixBeltGrid( defaultBelt, OUT_LINK,OUT_LINK,OUT_LINK,OUT_LINK, "BeltS2N");

            FixBeltGrid( defaultBelt, OUT_LINK,NO_LINK,NO_LINK,NO_LINK, "BeltS2N");
            FixBeltGrid( defaultBelt, NO_LINK,OUT_LINK,NO_LINK,NO_LINK, "BeltN2S");
            FixBeltGrid( defaultBelt, NO_LINK,NO_LINK,OUT_LINK,NO_LINK, "BeltW2E");
            FixBeltGrid( defaultBelt, NO_LINK,NO_LINK,NO_LINK,OUT_LINK, "BeltE2W");

            FixBeltGrid( defaultBelt, OUT_LINK,OUT_LINK,NO_LINK,NO_LINK, "BeltW2NS");
            FixBeltGrid( defaultBelt, NO_LINK,NO_LINK,OUT_LINK,OUT_LINK, "BeltS2EW");

            FixBeltGrid( defaultBelt, NO_LINK,OUT_LINK,OUT_LINK,NO_LINK, "BeltN2SE");
            FixBeltGrid( defaultBelt, OUT_LINK,NO_LINK,NO_LINK,OUT_LINK, "BeltS2NW");
            FixBeltGrid( defaultBelt, OUT_LINK,NO_LINK,OUT_LINK,NO_LINK, "BeltW2NE");
            FixBeltGrid( defaultBelt, NO_LINK,OUT_LINK,NO_LINK,OUT_LINK, "BeltE2SW");

            FixBeltGrid( defaultBelt, OUT_LINK,OUT_LINK,OUT_LINK,NO_LINK, "BeltW2NSE");
            FixBeltGrid( defaultBelt, NO_LINK,OUT_LINK,OUT_LINK,OUT_LINK, "BeltN2SEW");
            FixBeltGrid( defaultBelt, OUT_LINK,NO_LINK,OUT_LINK,OUT_LINK, "BeltS2NEW");
            FixBeltGrid( defaultBelt, OUT_LINK,OUT_LINK,NO_LINK,OUT_LINK, "BeltE2NSW");


            // sort the usable form list by in/out counts
            forms.Sort( FormInfoComparison );

            // print usable form list
            var valid = forms.Count;
            var show = new StringBuilder();

            var ix = 0;
            while ( ix < forms.Count ) {
                for (int num=0;num<6 && ix<valid;num++) show.Append(forms[ix++].Name).Append(",");
                show.Append("\n");
            }

            Console.WriteLine("Valid="+valid);
            Console.WriteLine(show);
            Console.WriteLine("\n\nDirectionArray:\n"+DefaultBeltAsCodeString(defaultBelt) );
        }

        internal string DefaultBeltAsCodeString(string[,,,] defaultBelt) {

            var buf = new StringBuilder();
            buf.Append("{\n");
            for (int nx=0;nx<3;nx++) {
                    buf.Append("\t{\n");
                for (int sx=0;sx<3;sx++) {
                    buf.Append("\t\t{\n");
                    for (int ex=0;ex<3;ex++) {
                        buf.Append("\t\t\t{ ");
                        for (int wx=0;wx<3;wx++) {
                            buf.Append(defaultBelt[nx,sx,ex,wx]);
                            if (wx<2) buf.Append(", ");
                        }
                        buf.Append( ex<2 ? " },\n" : " }\n" );
                    }
                    buf.Append( sx<2 ? "\t\t},\n" : "\t\t}\n" );
                }
                buf.Append( nx<2 ? "\t},\n" : "\t}\n" );
            }
            buf.Append("}\n");

            return buf.ToString();
        }

    }

    [TestClass]
    public class FacilityTypeInfoTests {


        [TestMethod]
        public void IsBelt() {

            var beltNo = new EmptySite() { };
            var beltYes = new BeltSite() { };

            IsTrue( SiteTypeInfo.IsBelt( beltYes ) ); 
            IsFalse( SiteTypeInfo.IsBelt( beltNo ) ); 

        }

        [TestMethod]
        public void EastDir() {

            AreEqual( IN_LINK, SiteTypeInfo.EastLink( new BeltSite(){ Type=BeltE2W } ) ); 
            AreEqual( OUT_LINK, SiteTypeInfo.EastLink( new BeltSite(){ Type=BeltW2E } ) ); 
            AreEqual( NO_LINK, SiteTypeInfo.EastLink( new BeltSite(){ Type=BeltN2S } ) ); 

            AreEqual( IN_LINK, SiteTypeInfo.EastLink( new BeltSite(){ Type=BeltNE2W } ) ); 
            AreEqual( OUT_LINK, SiteTypeInfo.EastLink( new BeltSite(){ Type=BeltW2SE } ) ); 
            AreEqual( NO_LINK, SiteTypeInfo.EastLink( new BeltSite(){ Type=BeltN2SW } ) ); 

        }

        [TestMethod]
        public void NorthDir() {

            AreEqual( IN_LINK, SiteTypeInfo.NorthLink(  new BeltSite(){ Type=BeltN2W } ) ); 
            AreEqual( OUT_LINK, SiteTypeInfo.NorthLink(  new BeltSite(){ Type=BeltW2N } ) ); 
            AreEqual( NO_LINK, SiteTypeInfo.NorthLink(  new BeltSite(){ Type=BeltE2S } ) ); 

            AreEqual( IN_LINK, SiteTypeInfo.NorthLink(  new BeltSite(){ Type=BeltNE2W } ) ); 
            AreEqual( OUT_LINK, SiteTypeInfo.NorthLink(  new BeltSite(){ Type=BeltW2NS } ) ); 
            AreEqual( NO_LINK, SiteTypeInfo.NorthLink(  new BeltSite(){ Type=BeltS2EW } ) ); 

        }
    }
}
