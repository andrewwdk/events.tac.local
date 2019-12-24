using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Data.Fields;
using Sitecore.Feature.Business.Models;

namespace Sitecore.Feature.Business.Builders
{
    public class MainNavigationBuilder : IMainNavigationBuilder
    {
        public MainNavigationItem Build(Item home)
        {
            if(home == null)
            {
                return null;
            }

            return new MainNavigationItem(home?.Fields["ContentHeading"].Value, LinkManager.GetItemUrl(home),
                (home != null) ? BuildChildren(home) : null);
        }

        private IEnumerable<MainNavigationItem> BuildChildren(Item node)
        {
            var children = node.GetChildren();
            var includedInNavigation = children.Where(i => i.Fields["ExcludeFromNavigation"] != null 
                && !((CheckboxField)i.Fields["ExcludeFromNavigation"]).Checked);
            return includedInNavigation.Select(i => new MainNavigationItem(i.Fields["ContentHeading"].Value, LinkManager.GetItemUrl(i), BuildChildren(i)));
        }
    }
}