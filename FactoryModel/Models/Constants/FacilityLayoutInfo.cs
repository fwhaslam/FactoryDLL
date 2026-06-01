// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Tools;

using static FactoryModel.Tools.SystemTools;
using static FactoryModel.Models.Constants.FacilityLayoutEnum;
using static FactoryModel.Models.Constants.LinkType;
using static FactoryModel.Models.Constants.DirectionEnum;

namespace FactoryModel.Models.Constants {

    public enum FacilityLayoutEnum {
        MakeEventTwoXTwo,
        VendEventTwoXTwo,
        MakerOneXOne,
        VendorOneXOne,
        EditorOneXOne,
    }

    /// <summary>
    /// Delta tile information for common Facility Layouts.
    /// </summary>
    public class FacilityLayoutInfo : KeyedElementIf<FacilityLayoutEnum> {


        public FacilityLayoutEnum Type { get; set; }

        public FacilityLayoutEnum Key { get { return Type; } }

        public List<Where> Tiles { get; set; }

        public Dictionary<Edge,LinkType> Links { get; set; }


        internal static List<FacilityLayoutInfo> LayoutList = new List<FacilityLayoutInfo>() {

            new FacilityLayoutInfo() {
                Type = MakeEventTwoXTwo,
                Tiles = new List<Where>() { new Where(0,0), new Where(1,0), new Where (0,1), new Where(1,1) }
            },
            new FacilityLayoutInfo() {
                Type = VendEventTwoXTwo,
                Tiles = new List<Where>() { new Where(0,0), new Where(1,0), new Where (0,1), new Where(1,1) }
            },
            new FacilityLayoutInfo() {
                Type = MakerOneXOne,
                Tiles = new List<Where>() { new Where(0,0) },
                Links = new Dictionary<Edge, LinkType>() {
                    { new Edge(0,0,East), OUT_LINK },
                }
            },
            new FacilityLayoutInfo() {
                Type = VendorOneXOne,
                Tiles = new List<Where>() { new Where(0,0) },
                Links = new Dictionary<Edge, LinkType>() {
                    { new Edge(0,0,West), IN_LINK },
                }
            },
            new FacilityLayoutInfo() {
                Type = EditorOneXOne,
                Tiles = new List<Where>() { new Where(0,0) },
                Links = new Dictionary<Edge, LinkType>() {
                    { new Edge(0,0,West), IN_LINK },
                    { new Edge(0,0,East), OUT_LINK },
                }
            }
        };

        public static  IDictionary<FacilityLayoutEnum,FacilityLayoutInfo> LayoutMap = 
            AsKeyedDictionary<FacilityLayoutEnum,FacilityLayoutInfo>( LayoutList );

    }
}
