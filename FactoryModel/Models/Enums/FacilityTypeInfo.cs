// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static FactoryModel.Models.Enums.FacilityTypeEnum;

namespace FactoryModel.Models.Enums {

    public class FacilityTypeInfo {

        // integer representing orientation of link between two connected facilities.
        public static readonly int NO_LINK = 0;
        public static readonly int OUT_LINK = 1;
        public static readonly int IN_LINK = 2;

        public static readonly int[] INVERSE_LINK = {0,2,1};

        // Dimensions are [3,3,3,3] using the Link Values[0/1/2]  Representing [N,S,E,W] directions.
        public static readonly FacilityTypeEnum[,,,] FacilityByDir =
        {
            {
                {
                    { BeltN2S, BeltE2W, BeltW2E },
                    { BeltW2E, BeltN2EW, BeltE2W },
                    { BeltE2W, BeltW2E, BeltEW2S }
                },
                {
                    { BeltN2S, BeltE2SW, BeltS2W },
                    { BeltN2SE, BeltN2SEW, BeltSE2W },
                    { BeltS2E, BeltSW2E, BeltS2EW }
                },
                {
                    { BeltS2N, BeltW2S, BeltSW2N },
                    { BeltE2S, BeltEW2S, BeltE2SW },
                    { BeltSE2W, BeltW2SE, BeltSEW2N }
                }
            },
            {
                {
                    { BeltS2N, BeltS2NW, BeltN2W },
                    { BeltW2NE, BeltS2NEW, BeltNE2W },
                    { BeltN2E, BeltNW2E, BeltN2EW }
                },
                {
                    { BeltE2NS, BeltE2NSW, BeltNS2W },
                    { BeltW2NSE, BeltN2S, BeltNSE2W },
                    { BeltNS2E, BeltNSW2E, BeltNS2EW }
                },
                {
                    { BeltN2S, BeltNW2S, BeltN2SW },
                    { BeltNE2S, BeltNEW2S, BeltNE2SW },
                    { BeltN2SE, BeltNW2SE, BeltN2SEW }
                }
            },
            {
                {
                    { BeltN2S, BeltW2N, BeltNW2E },
                    { BeltE2N, BeltEW2N, BeltE2NW },
                    { BeltNE2S, BeltW2NE, BeltNEW2S }
                },
                {
                    { BeltS2N, BeltSW2N, BeltS2NW },
                    { BeltSE2N, BeltSEW2N, BeltSE2NW },
                    { BeltS2NE, BeltSW2NE, BeltS2NEW }
                },
                {
                    { BeltNS2W, BeltW2NS, BeltNSW2E },
                    { BeltE2NS, BeltEW2NS, BeltE2NSW },
                    { BeltNSE2W, BeltW2NSE, BeltN2S }
                }
            }
        };


        static public bool IsEmpty( FacilityTypeEnum type ) {
            return type==Empty;
        }

        static public bool IsBelt( FacilityTypeEnum type ) {
            return type>=BeltE2W && type<=BeltNSW2E;
        }

        static public bool IsCross( FacilityTypeEnum type ) {
            return type>=CrossN2SxE2W && type<=CrossS2NxW2E;
        }

        /// <summary>
        /// East Out = +1, East In = -1, No East = 0
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>

        static public int EastLink( FacilityTypeEnum type ) {
            return IsDir( type, 'E' );
        }

        static public int WestLink( FacilityTypeEnum type ) {
            return IsDir( type, 'W' );
        }

        static public int NorthLink( FacilityTypeEnum type ) {
            return IsDir( type, 'N' );
        }

        static public int SouthLink( FacilityTypeEnum type ) {
            return IsDir( type, 'S' );
        }

        static internal int IsDir( FacilityTypeEnum type, char dir ) {
            if (!IsBelt(type)) return NO_LINK;
            var name = Enum.GetName( typeof(FacilityTypeEnum),type);
//Console.WriteLine("ISDIR = '"+name+"'");
            var split = name.Split('2');
            if (split[0].Contains(dir)) return IN_LINK;
            if (split[1].Contains(dir)) return OUT_LINK;
            return NO_LINK;
        }

        internal static string AsString( FacilityTypeEnum type ) {
            return nameof(type);
        }

    }
}
