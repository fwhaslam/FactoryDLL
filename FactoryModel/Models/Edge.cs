// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models.Constants;

namespace FactoryModel.Models {

    /// <summary>
    /// Each tile has four Edges, one for each DirectionEnum.
    /// Each Edge has a reciprocal, an adjacent tile with an opposite facing Edge.
    /// </summary>
    public class Edge : IEquatable<Edge>, IComparable<Edge> {

        public Edge( int x, int y, DirectionEnum d ) {
            this.X = x;
            this.Y = y;
            this.Dir = d;
        }
        public Edge( Where loc, DirectionEnum d ) {
            this.X = loc.X;
            this.Y = loc.Y;
            this.Dir = d;
        }

        public int X { get; set; }
        
        public int Y { get; set; }

        public DirectionEnum Dir { get; set; }

        public string ToDisplay( bool header = true ) { 
            var display = "x="+X+",y="+Y;
            if (!header) return display;
            return "Where("+display+")";
        }

        public override bool Equals(object obj) {
            return Equals(obj as Edge);
        }

        public bool Equals(Edge other) {
            return other != null &&
                   X == other.X &&
                   Y == other.Y &&
                   Dir == other.Dir;
        }

        /// <summary>
        /// Suitable for THIS GAME.  We do not plan to make maps greater than 10k on a side.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return ((( ((int)Dir)*10000) + X) * 10000 ) + Y;
        }

        public int CompareTo(Edge other) {
            return GetHashCode() - other.GetHashCode();
        }
    }
}
