// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FactoryModel.Models.Constants;
using FactoryModel.Models.Sites;
using static FactoryModel.Models.Constants.SiteGroup;
using static FactoryModel.Models.Constants.SiteType;
using static FactoryModel.Models.Constants.LinkType;

namespace FactoryModel.Models.Constants {

    // integer representing orientation of link between two connected facilities.
    public enum LinkType {
        NO_LINK = 0,
        OUT_LINK = 1,
        IN_LINK = 2,
        LinkTypeEnumCount=3
    }
    
    public enum DirectionEnum {
        North=0,  // +1 Y
        South=1,
        East=2,   // +1 X
        West=3,
        DirectionEnumCount=4
    };

    public class SiteTypeInfo {

        public static SiteType DEFAULT_BELT_ENUM = BeltS2N;

        public static int MAXIMUM_TIER_LIMIT = 6;

        public static readonly LinkType[] INVERSE_LINK = {NO_LINK,IN_LINK,OUT_LINK};

        public static List<Where> DirStep = new List<Where>() {
            new Where( 0, +1 ),     // North
            new Where( 0, -1 ),     // South
            new Where( +1, 0 ),     // East
            new Where( -1, 0 ),     // West
        };

        // Dimensions are [3,3,3,3] using the Link Values[0/1/2]  Representing [N,S,E,W] directions.
        public static readonly SiteType[,,,] SiteByDir =
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

        static public bool IsEmpty(Site build ) {
            return (build.Group==Empty);
        }

        /// <summary>
        /// TODO: replace this with a table
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static public bool IsBelt(Site build ) {
            return (build.Group==Belt);
        }

        static public bool IsCross(Site build ) {
            if (build.Group!=Belt) return false;
            var type = ((BeltSite)build).Type;
            return type>=CrossN2SxE2W && type<=CrossS2NxW2E;
        }

        /// <summary>
        /// East Out = +1, East In = -1, No East = 0
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>

        static public LinkType EastLink(Site build ) {
            return IsDir( build, 'E' );
        }

        static public LinkType InverseEastLink(Site build ) {
            return INVERSE_LINK[(int)IsDir( build, 'E' )];
        }

        static public LinkType WestLink(Site build ) {
            return IsDir( build, 'W' );
        }
        static public LinkType InverseWestLink(Site build ) {
            return INVERSE_LINK[(int)IsDir( build, 'W' )];
        }

        static public LinkType NorthLink(Site build ) {
            return IsDir( build, 'N' );
        }

        static public LinkType InverseNorthLink(Site build ) {
            return INVERSE_LINK[(int)IsDir( build, 'N' )];
        }

        static public LinkType SouthLink(Site build ) {
            return IsDir( build, 'S' );
        }

        static public LinkType InverseSouthLink(Site build ) {
            return INVERSE_LINK[(int)IsDir( build, 'S' )];
        }

        static internal LinkType IsDir(Site build, char dir ) {
            if (!IsBelt(build)) return NO_LINK;
            var type = ((BeltSite)build).Type; 
            var name = Enum.GetName( typeof(SiteType),type);
//Console.WriteLine("ISDIR = '"+name+"'");
            var split = name.Split('2');
            if (split[0].Contains(dir)) return IN_LINK;
            if (split[1].Contains(dir)) return OUT_LINK;
            return NO_LINK;
        }

        internal static string AsString( SiteType type ) {
            return nameof(type);
        }

    }
}
