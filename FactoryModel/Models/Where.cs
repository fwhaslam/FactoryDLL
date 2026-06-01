// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel.Models {

    public class Where : IEquatable<Where>, IComparable<Where> {

        // our map does not go negative, so -1/-1 is always 'off the map'
        static public Where NOWHERE = new Where(-1,-1);

        public Where(){}

        public Where( int x, int y ) {
            X = x;
            Y = y;
        }

        public string ToDisplay( bool header = true ) { 
            var display = "x="+X+",y="+Y;
            if (!header) return display;
            return "Where("+display+")";
        }

        public int X {  get; set; }

        public int Y {  get; set; }

        public Where Plus( Where add ) {
            return new Where( X+add.X, Y+add.Y );
        }

        public Where Minus( Where sub ) {
            return new Where( X-sub.X, Y-sub.Y );
        }

        public int Distance( Where to ) {
            return (int)( 0.5f + Math.Sqrt( Distance2(to) ) );
        }
        public int Distance2( Where to ) {
            var diff = Minus(to);
            return diff.X*diff.X + diff.Y*diff.Y;
        }

        public bool IsClose( Where to, int limit ) {
            return IsClose2( to, limit*limit );
        }

        public bool IsClose2( Where to, int limit2 ) {
            var dist2 = Distance2( to );
            return ( dist2 < limit2 );
        }

        public override bool Equals(object obj) {
            return Equals(obj as Where);
        }

        public bool Equals(Where other) {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }

        /// <summary>
        /// Suitable for THIS GAME.  We do not plan to make maps greater than 10k on a side.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return (X*10000) + Y;
        }

        public int CompareTo(Where other) {
            return GetHashCode() - other.GetHashCode();
        }
    }
}
