using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models;

namespace FactoryModel.Builder {

    /// <summary>
    /// Collection of tiles with proposed facility changes.
    /// Searchable by location, and distinct by location.
    /// </summary>
    public class FacilityPlan : Dictionary<Where,Tile> { 

        /// <summary>
        /// Add or replace a tile.
        /// </summary>
        /// <param name="work"></param>
        public void Put( Tile work ) {
            var found = RemoveAt( work.Loc );
            this[work.Loc] = work;
        }

        public Tile RemoveAt( Where loc ) {
            if (!ContainsKey(loc)) return null;
            var found = this[loc];
            Remove(loc);
            return found;
        }

    }
}
