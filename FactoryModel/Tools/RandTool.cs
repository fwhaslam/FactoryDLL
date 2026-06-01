// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel.Tools {

    public class RandTool : Random {

        public RandTool( int seed ) : base(seed) { }

        public float NextFloat() { return (float)Sample(); }

        public T Pick<T>( List<T> list ) {
            return list[  Next(list.Count) ];
        }

    }
}
