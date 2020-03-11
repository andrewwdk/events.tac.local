using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Business.Builders;
using Sitecore.Feature.Business.Models;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

namespace Sitecore.Feature.Business
{
    public class RelatedEventsProvider : IRelatedEventsProvider
    {
        private readonly IRenderingContext _context;
        public RelatedEventsProvider(IRenderingContext context)
        {
            _context = context;
        }

        public IEnumerable<NavigationItem> GetRelatedEvents()
        {
            var items = Enumerable.Empty<NavigationItem>();

            if (_context != null && _context?.ContextItem != null)
            {
                if (_context.ContextItem.GetMultilistFieldItems("RelatedEvents") != null)
                {
                    items = _context.ContextItem.GetMultilistFieldItems("RelatedEvents").
                                Select(i => new NavigationItem(i.DisplayName, i.Url));
                }
            }

            return items;
        }
    }
}