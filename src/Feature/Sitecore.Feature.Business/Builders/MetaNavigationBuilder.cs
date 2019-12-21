using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Feature.Business.Models;
using Sitecore.Feature.Business.Builder_Interfaces;
using Sitecore.Links;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

namespace Sitecore.Feature.Business.Builders
{
    public class MetaNavigationBuilder : IMetaNavigationBuilder
    {
        public IEnumerable<MetaNavigationItem> Build(Item home)
        {
            string title, url;
            var pages = new List<MetaNavigationItem>();
            MultilistField multiselectField = home.Fields["MetaPages"];
            Item[] items = multiselectField.GetItems();

            if (items != null)
            {
                foreach (var item in items)
                {
                    title = item.Fields["ContentHeading"].Value;
                    url = LinkManager.GetItemUrl(item);
                    pages.Add(new MetaNavigationItem(title, url));
                }
            }

            return pages;
        }
    }
}