using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models.Constants;

namespace FactoryModel.Models.Sites {

    /// <summary>
    /// Multi-Tile facility use a FacilityLayout and an orientation 
    /// to determine tile and edge locations.
    /// </summary>
    public class BigSite : Site {

        override public SiteGroup Group { get { return Core.Group; } }

        override public SiteType Type { get {  return Core.Type; } }

        /// <summary>
        /// Upper left corner of layout.
        /// Lowest X/Y combination.
        /// </summary>
        public Where ULC { get; set; }

        public Site Core { get; set; }

        public List<Where> Tiles { get; set; }

        public Dictionary< Edge, LinkType > Links { get; set; }

    }

}
