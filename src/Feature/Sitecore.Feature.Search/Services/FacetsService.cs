using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Feature.Search.Models;

namespace Sitecore.Feature.Search.Services
{
    public class FacetsService : IFacetsService
    {
        public Facets GetFacets(ISearchIndex index, string search)
        {
            if(search == null)
            {
                search = string.Empty;
            }

            using (var context = index.CreateSearchContext())
            {
                var result = context.GetQueryable<EventDetails>().Where(j => j.Name.Contains(search))
                    .FacetOn(j => j.Duration, 1).GetResults();
                var durations = result.Facets.Categories[0].Values.
                    Select(f => new Facet() { Name = f.Name, Count = f.AggregateCount });
                result = context.GetQueryable<EventDetails>().Where(j => j.Name.Contains(search))
                    .FacetOn(j => j.DifficultyLevel, 1).GetResults();
                var diffLevels = result.Facets.Categories[0].Values.
                    Select(f => new Facet() { Name = f.Name, Count = f.AggregateCount });
                return new Facets()
                {
                    DurationFacets = durations.OrderByDescending(i => i.Count).ThenBy(i => i.Name).ToList(),
                    DifficultyFacets = diffLevels.OrderByDescending(i => i.Count).ThenBy(i => i.Name).ToList()
                };
            }
        }
    }
}