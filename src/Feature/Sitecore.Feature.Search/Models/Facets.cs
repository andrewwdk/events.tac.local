using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Search.Models
{
    public class Facets
    {
        public List<Facet> DurationFacets { get; set; }
        public List<Facet> DifficultyFacets { get; set; }
    }
}