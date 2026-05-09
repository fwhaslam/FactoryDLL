// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using static VerboseCSharp.Asserts.StringAsserts;

using System;
using System.Linq;

using System.Text;
using FactoryModel.Builder;

using System.Collections.Generic;
using static FactoryModel.Models.Constants.FacilityTypeEnum;
using static FactoryModel.Models.Constants.FacilityTypeInfo;
using FactoryModel.Models.Constants;

namespace FactoryModelTests.Models {

    [TestClass]
    public class FacilityTypeEnumTests : FacilityTypeInfo {

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
            for (int nx = 0; nx < 3; nx++) {
                for (int sx = 0; sx < 3; sx++) {
                    for (int ex = 0; ex < 3; ex++) {

                        for (int wx = 0; wx < 3; wx++) {

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

                            defaultBelt[nx,sx,ex,wx] = name;
                        }
                    }
                }
            }

            // replace 'empty' values with more useful defaults
            // no ins + no outs
            defaultBelt[NO_LINK,NO_LINK,NO_LINK,NO_LINK] = "BeltS2N";   // see: DEFAULT_BELT_ENUM

            // no outs
            defaultBelt[IN_LINK,IN_LINK,IN_LINK,IN_LINK] = "BeltS2N";

            defaultBelt[IN_LINK,NO_LINK,NO_LINK,NO_LINK] = "BeltN2S";
            defaultBelt[NO_LINK,IN_LINK,NO_LINK,NO_LINK] = "BeltS2N";
            defaultBelt[NO_LINK,NO_LINK,IN_LINK,NO_LINK] = "BeltE2W";
            defaultBelt[NO_LINK,NO_LINK,NO_LINK,IN_LINK] = "BeltW2E";

            defaultBelt[IN_LINK,IN_LINK,NO_LINK,NO_LINK] = "BeltNS2E";
            defaultBelt[NO_LINK,NO_LINK,IN_LINK,IN_LINK] = "BeltEW2N";

            defaultBelt[NO_LINK,IN_LINK,IN_LINK,NO_LINK] = "BeltSE2W";
            defaultBelt[IN_LINK,NO_LINK,NO_LINK,IN_LINK] = "BeltNW2E";
            defaultBelt[IN_LINK,NO_LINK,IN_LINK,NO_LINK] = "BeltNE2S";
            defaultBelt[NO_LINK,IN_LINK,NO_LINK,IN_LINK] = "BeltSW2N";

            defaultBelt[IN_LINK,IN_LINK,IN_LINK,NO_LINK] = "BeltNSE2W";
            defaultBelt[NO_LINK,IN_LINK,IN_LINK,IN_LINK] = "BeltSEW2N";
            defaultBelt[IN_LINK,NO_LINK,IN_LINK,IN_LINK] = "BeltNEW2S";
            defaultBelt[IN_LINK,IN_LINK,NO_LINK,IN_LINK] = "BeltNSW2E";

            // no ins
            defaultBelt[OUT_LINK,OUT_LINK,OUT_LINK,OUT_LINK] = "BeltS2N";

            defaultBelt[OUT_LINK,NO_LINK,NO_LINK,NO_LINK] = "BeltS2N";
            defaultBelt[NO_LINK,OUT_LINK,NO_LINK,NO_LINK] = "BeltN2S";
            defaultBelt[NO_LINK,NO_LINK,OUT_LINK,NO_LINK] = "BeltW2E";
            defaultBelt[NO_LINK,NO_LINK,NO_LINK,OUT_LINK] = "BeltE2W";

            defaultBelt[OUT_LINK,OUT_LINK,NO_LINK,NO_LINK] = "BeltW2NS";
            defaultBelt[NO_LINK,NO_LINK,OUT_LINK,OUT_LINK] = "BeltS2EW";

            defaultBelt[NO_LINK,OUT_LINK,OUT_LINK,NO_LINK] = "BeltN2SE";
            defaultBelt[OUT_LINK,NO_LINK,NO_LINK,OUT_LINK] = "BeltS2NW";
            defaultBelt[OUT_LINK,NO_LINK,OUT_LINK,NO_LINK] = "BeltW2NE";
            defaultBelt[NO_LINK,OUT_LINK,NO_LINK,OUT_LINK] = "BeltE2SW";

            defaultBelt[OUT_LINK,OUT_LINK,OUT_LINK,NO_LINK] = "BeltW2NSE";
            defaultBelt[NO_LINK,OUT_LINK,OUT_LINK,OUT_LINK] = "BeltN2SEW";
            defaultBelt[OUT_LINK,NO_LINK,OUT_LINK,OUT_LINK] = "BeltS2NEW";
            defaultBelt[OUT_LINK,OUT_LINK,NO_LINK,OUT_LINK] = "BeltE2NSW";


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

            IsTrue( FacilityTypeInfo.IsBelt( BeltE2W ) ); 
            IsTrue( FacilityTypeInfo.IsBelt( BeltSE2N ) ); 
            IsTrue( FacilityTypeInfo.IsBelt( BeltNSW2E ) ); 


            IsFalse( FacilityTypeInfo.IsBelt( Empty ) ); 
            IsFalse( FacilityTypeInfo.IsBelt( CrossN2SxE2W ) ); 
            IsFalse( FacilityTypeInfo.IsBelt( CrusherW2E ) ); 

        }

        [TestMethod]
        public void EastDir() {

            AreEqual( IN_LINK, FacilityTypeInfo.EastLink( BeltE2W ) ); 
            AreEqual( OUT_LINK, FacilityTypeInfo.EastLink( BeltW2E ) ); 
            AreEqual( NO_LINK, FacilityTypeInfo.EastLink( BeltN2S ) ); 

            AreEqual( IN_LINK, FacilityTypeInfo.EastLink( BeltNE2W ) ); 
            AreEqual( OUT_LINK, FacilityTypeInfo.EastLink( BeltW2SE ) ); 
            AreEqual( NO_LINK, FacilityTypeInfo.EastLink( BeltN2SW ) ); 

        }

        [TestMethod]
        public void NorthDir() {

            AreEqual( IN_LINK, FacilityTypeInfo.NorthLink( BeltN2W ) ); 
            AreEqual( OUT_LINK, FacilityTypeInfo.NorthLink( BeltW2N ) ); 
            AreEqual( NO_LINK, FacilityTypeInfo.NorthLink( BeltE2S ) ); 

            AreEqual( IN_LINK, FacilityTypeInfo.NorthLink( BeltNE2W ) ); 
            AreEqual( OUT_LINK, FacilityTypeInfo.NorthLink( BeltW2NS ) ); 
            AreEqual( NO_LINK, FacilityTypeInfo.NorthLink( BeltS2EW ) ); 

        }
    }
}
