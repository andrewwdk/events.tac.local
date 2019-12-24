using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Language.Models
{
    public class Language
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public Language(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}