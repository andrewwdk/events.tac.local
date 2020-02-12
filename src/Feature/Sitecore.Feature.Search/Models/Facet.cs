using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Search.Models
{
    public class Facet
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int Count { get; set; }
    }
}