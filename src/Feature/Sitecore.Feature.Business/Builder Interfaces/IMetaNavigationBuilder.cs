using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Feature.Business.Models;

namespace Sitecore.Feature.Business.Builder_Interfaces
{
    public interface IMetaNavigationBuilder
    {
        IEnumerable<MetaNavigationItem> Build(Item home);
    }
}