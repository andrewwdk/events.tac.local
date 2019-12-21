using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Feature.Business.Builders;
using Sitecore.Feature.Business.Builder_Interfaces;

namespace Sitecore.Feature.Business.Controllers
{
    public class MetaNavigationController : Controller
    {
        private readonly IMetaNavigationBuilder _builder;

        public MetaNavigationController(IMetaNavigationBuilder builder)
        {
            _builder = builder;
        }

        // GET: MetaNavigation
        public ActionResult Index()
        {
            var home = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            return PartialView("~/Views/MetaNavigation/Index.cshtml", _builder.Build(home));
        }
    }
}