using Sitecore.Feature.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.Business.Builders
{
    public interface IRelatedEventsProvider
    {
        IEnumerable<NavigationItem> GetRelatedEvents();
    }
}
