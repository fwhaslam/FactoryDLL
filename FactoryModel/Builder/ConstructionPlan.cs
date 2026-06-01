using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models;
using FactoryModel.Models.Constants;

namespace FactoryModel.Builder {

    /// <summary>
    /// Collection of tiles with proposed site changes.
    /// Searchable by location, and distinct by location.
    /// </summary>
    public class ConstructionPlan : Dictionary<Where,Tile> { 

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

        /// <summary>
        /// Return existing tile, or create an EmptySite tile as a placeholder, and return that.
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public Tile FixBeltTile( Where loc, SiteType beltType ) {
            if (!ContainsKey(loc)) {
                this[loc] = new Tile() { 
                    Loc = loc,
                    Site = SiteFactory.MakeBeltSite(beltType)
                };
            }
            return this[loc];
        }

    }
}
