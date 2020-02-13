using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Web.UI.WebControls;

namespace Sitecore.Feature.Search.Models
{
    public class EventDetails : SearchResultItem
    {
        public string ContentHeading { get; set; }
        public string ContentIntro { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public int DifficultyLevel { get; set; }
        public int ComputedContentLength { get; set; }
        public string ItemUrl { get; set; }
        public HtmlString Eventmage
        { 
            get
            {
                return new HtmlString(FieldRenderer.Render(GetItem(), "EventImage", "DisableWebEditing=true&mw=150"));
            }
        }
    }
}