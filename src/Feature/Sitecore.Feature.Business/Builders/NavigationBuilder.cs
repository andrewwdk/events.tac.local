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
    public class NavigationBuilder : INavigationBuilder
    {
        private readonly IRenderingContext _context;
       // public NavigationBuilder() : this(SitecoreRenderingContext.Create()) { }
        public NavigationBuilder(IRenderingContext context)
        {
            _context = context;
        }

        public NavigationMenuItem Build()
        {
            var root = _context?.DatasourceOrContextItem;

            return new NavigationMenuItem(root?.DisplayName, root?.Url,
                (root != null && _context?.ContextItem != null) ? Build(root, _context?.ContextItem) : null);
        }

        public IEnumerable<NavigationMenuItem> Build(IItem node, IItem current)
        {
            return node.GetChildren().Select(i => new NavigationMenuItem(i.DisplayName, i.Url,
                i.IsAncestorOrSelf(current) ? Build(i, current) : null));
        }
    }
}