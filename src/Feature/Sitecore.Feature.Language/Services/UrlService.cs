using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Sitecore.Feature.Language.Models;
using Sitecore.Sites;
using Sitecore.Web;

namespace Sitecore.Feature.Language.Services
{
    public class UrlService : IUrlService
    {
        public NameValueCollection ReplaceQueryStringParam(string currentPageUrl, string paramToReplace, string newValue)
        {
            string queryString = currentPageUrl.IndexOf('?') >= 0
                ? currentPageUrl.Substring(currentPageUrl.IndexOf('?'))
                : null;

            var queryParamList = queryString != null
                ? HttpUtility.ParseQueryString(queryString)
                : HttpUtility.ParseQueryString(string.Empty);

            if (queryParamList[paramToReplace] != null)
            {
                queryParamList[paramToReplace] = newValue;
            }
            else
            {
                queryParamList.Add(paramToReplace, newValue);
            }

            return queryParamList;
        }
    }
}