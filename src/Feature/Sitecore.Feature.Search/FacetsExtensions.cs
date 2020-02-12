using Sitecore.Feature.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Search
{
    public static class FacetsExtensions
    {
        public static void Mark(this Facets facets, string[] durations, string[] difficulties)
        {
            if (durations != null && durations.Count() > 0)
            {
                foreach (var facet in facets.DurationFacets)
                {
                    if (durations.Contains(facet.Name))
                    {
                        facet.IsChecked = true;
                    }
                }
            }

            if (difficulties != null && difficulties.Count() > 0)
            {
                foreach (var facet in facets.DifficultyFacets)
                {
                    if (difficulties.Contains(facet.Name))
                    {
                        facet.IsChecked = true;
                    }
                }
            }
        }
    }
}