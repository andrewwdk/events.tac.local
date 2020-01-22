using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Feature.Business.Builders;

namespace Sitecore.Feature.Business.Controllers
{
    public class MainNavigationController : Controller
    {
        private readonly IMainNavigationBuilder _builder;

        public MainNavigationController(IMainNavigationBuilder builder)
        {
            _builder = builder;
        }

        // GET: MainNavigation
        public ActionResult Index()
        {
            var home = Context.Database.GetItem(Context.Site.StartPath);
            return PartialView("~/Views/MainNavigation/Index.cshtml", _builder.Build(home));
        }
    }
}