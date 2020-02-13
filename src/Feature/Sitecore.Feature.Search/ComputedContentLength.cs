using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Search
{
    public class ComputedContentLength : AbstractComputedIndexField
    {
        public override object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;

            if (item == null)
            {
                return null;
            }

            int count = 0;

            if (item.Fields["ContentBody"] != null)
            {
                var content = item.Fields["ContentBody"].ToString();
                if(content != string.Empty)
                {
                    count = content.Split(' ').Length;
                }
            }

            return count;
        }
    }
}