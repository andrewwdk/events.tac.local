using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Business.Models
{
    public class MetaNavigationItem : NavigationItem
    {
        public MetaNavigationItem(string title, string url)
            :base(title, url, false)
        {

        }
    }
}