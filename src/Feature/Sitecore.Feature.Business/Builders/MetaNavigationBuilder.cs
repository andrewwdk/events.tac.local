﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Feature.Business.Models;
using Sitecore.Links;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

namespace Sitecore.Feature.Business.Builders
{
    public class MetaNavigationBuilder : IMetaNavigationBuilder
    {
        public IEnumerable<MetaNavigationItem> Build(Item datasource)
        {
            if (datasource == null)
            {
                return null;
            }

            string title, url;
            var pages = new List<MetaNavigationItem>();
            MultilistField multiselectField = datasource.Fields["MetaPages"];

            if(multiselectField == null)
            {
                return null;
            }

            Item[] items = multiselectField.GetItems();

            if (items != null)
            {
                foreach (var item in items)
                {
                    title = item.Fields["ContentHeading"].Value ?? string.Empty;
                    url = LinkManager.GetItemUrl(item);
                    pages.Add(new MetaNavigationItem(title, url));
                }
            }

            return pages; 
        }
    }
}