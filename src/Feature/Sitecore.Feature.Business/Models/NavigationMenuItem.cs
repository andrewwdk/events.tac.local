using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Business.Models
{
    public class NavigationMenuItem : NavigationItem
    {
        public IEnumerable<NavigationMenuItem> Children { get; private set; }

        public NavigationMenuItem(string title, string url, IEnumerable<NavigationMenuItem> children)
            : base(title, url, false)
        {
            Children = children;
        }
    }
}