// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static FactoryModel.Models.Constants.FacilityTypeEnum;

namespace FactoryModel.Models.Constants {

    public class FacilityTypeInfo {

        // integer representing orientation of link between two connected facilities.
        public static readonly int NO_LINK = 0;
        public static readonly int OUT_LINK = 1;
        public static readonly int IN_LINK = 2;

        public static readonly int[] INVERSE_LINK = {NO_LINK,IN_LINK,OUT_LINK};

        // Dimensions are [3,3,3,3] using the Link Values[0/1/2]  Representing [N,S,E,W] directions.
        public static readonly FacilityTypeEnum[,,,] FacilityByDir =
        {
    	    {
    		    {
    			    { BeltS2N, BeltE2W, BeltW2E },
    			    { BeltW2E, BeltS2EW, BeltW2E },
    			    { BeltE2W, BeltE2W, BeltEW2N }
    		    },
    		    {
    			    { BeltN2S, BeltE2SW, BeltW2S },
    			    { BeltN2SE, BeltN2SEW, BeltW2SE },
    			    { BeltE2S, BeltE2SW, BeltEW2S }
    		    },
    		    {
    			    { BeltS2N, BeltS2W, BeltSW2N },
    			    { BeltS2E, BeltS2EW, BeltSW2E },
    			    { BeltSE2W, BeltSE2W, BeltSEW2N }
    		    }
    	    },
    	    {
    		    {
    			    { BeltS2N, BeltS2NW, BeltW2N },
    			    { BeltW2NE, BeltS2NEW, BeltW2NE },
    			    { BeltE2N, BeltE2NW, BeltEW2N }
    		    },
    		    {
    			    { BeltW2NS, BeltE2NSW, BeltW2NS },
    			    { BeltW2NSE, BeltS2N, BeltW2NSE },
    			    { BeltE2NS, BeltE2NSW, BeltEW2NS }
    		    },
    		    {
    			    { BeltS2N, BeltS2NW, BeltSW2N },
    			    { BeltS2NE, BeltS2NEW, BeltSW2NE },
    			    { BeltSE2N, BeltSE2NW, BeltSEW2N }
    		    }
    	    },
    	    {
    		    {
    			    { BeltN2S, BeltN2W, BeltNW2E },
    			    { BeltN2E, BeltN2EW, BeltNW2E },
    			    { BeltNE2S, BeltNE2W, BeltNEW2S }
    		    },
    		    {
    			    { BeltN2S, BeltN2SW, BeltNW2S },
    			    { BeltN2SE, BeltN2SEW, BeltNW2SE },
    			    { BeltNE2S, BeltNE2SW, BeltNEW2S }
    		    },
    		    {
    			    { BeltNS2E, BeltNS2W, BeltNSW2E },
    			    { BeltNS2E, BeltNS2EW, BeltNSW2E },
    			    { BeltNSE2W, BeltNSE2W, BeltS2N }
    		    }
    	    }
        };

        static public bool IsEmpty( FacilityTypeEnum type ) {
            return type==Empty;
        }

        /// <summary>
        /// TODO: replace this with a table
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static public bool IsBelt( FacilityTypeEnum type ) {
            return type>FacilityTypeEnumInfo.BEFORE_BELTS && type<FacilityTypeEnumInfo.AFTER_BELTS;
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

        static public int InverseEastLink( FacilityTypeEnum type ) {
            return INVERSE_LINK[IsDir( type, 'E' )];
        }

        static public int WestLink( FacilityTypeEnum type ) {
            return IsDir( type, 'W' );
        }
        static public int InverseWestLink( FacilityTypeEnum type ) {
            return INVERSE_LINK[IsDir( type, 'W' )];
        }

        static public int NorthLink( FacilityTypeEnum type ) {
            return IsDir( type, 'N' );
        }

        static public int InverseNorthLink( FacilityTypeEnum type ) {
            return INVERSE_LINK[IsDir( type, 'N' )];
        }

        static public int SouthLink( FacilityTypeEnum type ) {
            return IsDir( type, 'S' );
        }

        static public int InverseSouthLink( FacilityTypeEnum type ) {
            return INVERSE_LINK[IsDir( type, 'S' )];
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
