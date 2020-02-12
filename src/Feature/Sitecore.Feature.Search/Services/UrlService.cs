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

        public string SetParamAndValue(string currentPageUrl, string param, string value)
        {
            string basicUrl = currentPageUrl.IndexOf('?') >= 0
                ? currentPageUrl.Substring(0, currentPageUrl.IndexOf('?') + 1)
                : currentPageUrl + "?";

            string queryString = currentPageUrl.IndexOf('?') >= 0
                ? currentPageUrl.Substring(currentPageUrl.IndexOf('?'))
                : null;

            var queryParamList = queryString != null
                ? HttpUtility.ParseQueryString(queryString)
             : HttpUtility.ParseQueryString(string.Empty);
            
            queryParamList.Add(param, value);

            return basicUrl + queryParamList;
        }

        public string RemoveParams(string currentPageUrl, params string[] parameters)
        {
            string basicUrl = currentPageUrl.IndexOf('?') >= 0
                ? currentPageUrl.Substring(0, currentPageUrl.IndexOf('?') + 1)
                : currentPageUrl + "?";

            string queryString = currentPageUrl.IndexOf('?') >= 0
                ? currentPageUrl.Substring(currentPageUrl.IndexOf('?'))
                : null;

            var queryParamList = queryString != null
                ? HttpUtility.ParseQueryString(queryString)
             : HttpUtility.ParseQueryString(string.Empty);

            foreach (var param in parameters)
            {
                if (queryParamList.AllKeys.Contains(param))
                {
                    queryParamList.Remove(param);
                }
            }

            return basicUrl + queryParamList;
        }
    }
}