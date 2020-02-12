using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sitecore.Feature.Search.Services
{
    interface IUrlService
    {
        string GetParamValue(string currentPageUrl, string param);

        void SetParamAndValue(string currentPageUrl, string param, string value);
    }
}
