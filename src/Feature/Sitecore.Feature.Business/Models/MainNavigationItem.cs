using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Business.Models
{
    public class MainNavigationItem : NavigationMenuItem
    {
        public MainNavigationItem(string title, string url, IEnumerable<MainNavigationItem> children/*, bool excludeFromnavigation*/)
            :base(title, url, children)
        {
        }
    }
}