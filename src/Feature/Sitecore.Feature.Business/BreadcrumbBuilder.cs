using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Business.Models;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

namespace Sitecore.Feature.Business
{
    public class BreadcrumbBuilder
    {
        private readonly IRenderingContext _context;
        public BreadcrumbBuilder() : this(SitecoreRenderingContext.Create()) { }
        public BreadcrumbBuilder(IRenderingContext context)
        {
            _context = context;
        }

        public IEnumerable<NavigationItem> Build()
        {
            IEnumerable<NavigationItem> items = null;

            if (_context?.ContextItem == null || _context?.HomeItem == null)
            {
                items = Enumerable.Empty<NavigationItem>();
            }
            else
            {
                items = _context.ContextItem.GetAncestors().Where(i => _context.HomeItem.IsAncestorOrSelf(i))
                    .Select(i => new NavigationItem(i.DisplayName, i.Url, false))
                    .Concat(new[] { new NavigationItem(_context.ContextItem.DisplayName, _context.ContextItem.Url, true) });
            }

            return items;
        }

    }
}