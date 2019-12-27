using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Sitecore.Feature.Language.Models;
using Sitecore.Sites;

namespace Sitecore.Feature.Language.Services
{
    public interface IUrlService
    {
        NameValueCollection ReplaceQueryStringParam(string currentPageUrl, string paramToReplace, string newValue);
    }
}