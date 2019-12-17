using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Business.Models;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

namespace Sitecore.Feature.Business
{
    public class MainNavigationBuilder
    {
        private readonly IRenderingContext _context;
        public MainNavigationBuilder() : this(SitecoreRenderingContext.Create()) { }
        public MainNavigationBuilder(IRenderingContext context)
        {
            _context = context;
        }

        public MainNavigationItem Build()
        {
            var home = _context?.HomeItem;
            return new MainNavigationItem(home?.DisplayName, home?.Url,
                (home != null) ? Build(home) : null);
        }

        public IEnumerable<MainNavigationItem> Build(IItem node)
        {
            var children = node.GetChildren();
            var includedInNavigation = children.Where(i => i["ExcludeFromNavigation"] != "1");
            return includedInNavigation.Select(i => new MainNavigationItem(i.DisplayName, i.Url, Build(i)));
        }
    }
}