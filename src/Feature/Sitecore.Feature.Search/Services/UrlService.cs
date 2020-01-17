using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Search.Services
{
    public class UrlService
    {
        public string GetParamValue(string currentPageUrl, string param)
        {
            string queryString = currentPageUrl.IndexOf('?') >= 0
                ? currentPageUrl.Substring(currentPageUrl.IndexOf('?'))
                : null;

            var queryParamList = queryString != null
                ? HttpUtility.ParseQueryString(queryString)
             : HttpUtility.ParseQueryString(string.Empty);

            return (queryParamList[param] != null) ? queryParamList[param] : null;
        }
}
}