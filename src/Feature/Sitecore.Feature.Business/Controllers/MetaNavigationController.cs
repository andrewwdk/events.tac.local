using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Feature.Business.Models;
using Sitecore.Web.UI.WebControls;
using Sitecore.Links;

namespace Sitecore.Feature.Business.Controllers
{
    public class MetaNavigationController : Controller
    {
        private readonly MetaNavigationBuilder _builder;

        public MetaNavigationController(MetaNavigationBuilder builder)
        {
            _builder = builder;
        }

        public MetaNavigationController() : this(new MetaNavigationBuilder()) { }

        // GET: MetaNavigation
        public ActionResult Index()
        {
            string title, url;
            var pages = new List<MetaNavigationItem>();
            var home1 = _builder.Build();
            Sitecore.Data.Database master = Sitecore.Configuration.Factory.GetDatabase("master");
            Sitecore.Data.Items.Item home = master.GetItem("/sitecore/content/Home");
            MultilistField multiselectField = home.Fields["MetaPages"];
            Item[] items = multiselectField.GetItems();

            if (items != null)
            {
                foreach (var item in items)
                {
                    title = item.Fields["ContentHeading"].Value;
                    url = LinkManager.GetItemUrl(item);
                    pages.Add(new MetaNavigationItem(title, url));
                }
            }

            return PartialView("~/Views/MetaNavigation/Index.cshtml", pages);
        }
    }
}