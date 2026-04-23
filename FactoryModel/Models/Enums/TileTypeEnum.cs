// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel.Models.Enums {

    public enum TileTypeEnum {
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

        public static readonly int TileTypeEnumCount = Enum.GetNames(typeof(TileTypeEnum)).Length;

        public static readonly Dictionary<TileTypeEnum,TileTypeInfo> InfoMap = 
                new Dictionary<TileTypeEnum,TileTypeInfo>() {
            { TileTypeEnum.Sea, new TileTypeInfo(){ BaseHeight=0 } },
            { TileTypeEnum.Swamp, new TileTypeInfo(){ BaseHeight=1 } },
            { TileTypeEnum.Dunes, new TileTypeInfo(){ BaseHeight=2 } },
            { TileTypeEnum.Plains, new TileTypeInfo(){ BaseHeight=3 } },
            { TileTypeEnum.Woods, new TileTypeInfo(){ BaseHeight=4 } },
            { TileTypeEnum.Forest, new TileTypeInfo(){ BaseHeight=5 } },
            { TileTypeEnum.Hills, new TileTypeInfo(){ BaseHeight=6 } },
            { TileTypeEnum.Peaks, new TileTypeInfo(){ BaseHeight=7 } },
        };

        public int BaseHeight {  get; set; }

    }
}
