using Sitecore.ContentSearch;
using Sitecore.Feature.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.Search.Services
{
   public  interface IFacetsService
    {
        Facets GetFacets(ISearchIndex index, string search);
    }
}
