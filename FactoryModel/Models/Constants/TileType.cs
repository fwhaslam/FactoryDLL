// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static FactoryModel.Models.Constants.TileType;

namespace FactoryModel.Models.Constants {

    public enum TileType {
        Sea,
        Plains,
        Woods,
        Forest,
        Swamp,
        Dunes,
        Hills,
        Peaks
    }

    public class TileTypeInfo {

        public static readonly int TileTypeEnumCount = Enum.GetNames(typeof(TileType)).Length;

        public static readonly Dictionary<TileType,TileTypeInfo> InfoMap = 
                new Dictionary<TileType,TileTypeInfo>() {
            { TileType.Sea, new TileTypeInfo(){ BaseHeight=0 } },
            { TileType.Swamp, new TileTypeInfo(){ BaseHeight=1 } },
            { TileType.Dunes, new TileTypeInfo(){ BaseHeight=2 } },
            { TileType.Plains, new TileTypeInfo(){ BaseHeight=3 } },
            { TileType.Woods, new TileTypeInfo(){ BaseHeight=4 } },
            { TileType.Forest, new TileTypeInfo(){ BaseHeight=5 } },
            { TileType.Hills, new TileTypeInfo(){ BaseHeight=6 } },
            { TileType.Peaks, new TileTypeInfo(){ BaseHeight=7 } },
        };

        public int BaseHeight {  get; set; }


        public static bool IsBuildSite( TileType type ) {
            if (type==Sea) return false;
            if (type==Peaks) return false;
            if (type==Swamp) return false;
            return true;
        }

        public static bool NoBuildSite( TileType type ) {
            if (type==Sea || type==Peaks || type==Swamp) return true;
            return false;
        }

        public static bool IsBuildBelt( TileType type ) {
            if (type==Sea) return false;
            if (type==Peaks) return false;
            return true;
        }

        public static bool NoBuildBelt( TileType type ) {
            if (type==Sea || type==Peaks) return true;
            return false;
        }
    }
}
