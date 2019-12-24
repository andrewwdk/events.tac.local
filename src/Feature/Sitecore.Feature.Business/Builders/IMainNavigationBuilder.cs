using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Feature.Business.Models;

namespace Sitecore.Feature.Business.Builders
{
    public interface IMainNavigationBuilder
    {
        MainNavigationItem Build(Item home);
    }
}