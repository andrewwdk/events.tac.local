using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Business.Models;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

namespace Sitecore.Feature.Business
{
    public class RelatedEventsProvider
    {
        private readonly IRenderingContext _context;
        public RelatedEventsProvider() : this(SitecoreRenderingContext.Create()) { }
        public RelatedEventsProvider(IRenderingContext context)
        {
            _context = context;
        }

        public IEnumerable<NavigationItem> GetRelatedEvents()
        {
            return _context.ContextItem.GetMultilistFieldItems("RelatedEvents").Select(i => new NavigationItem(i.DisplayName, i.Url));
        }
    }
}