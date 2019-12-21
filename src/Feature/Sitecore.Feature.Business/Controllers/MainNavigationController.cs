using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Feature.Business.Builders;
using Sitecore.Feature.Business.Builder_Interfaces;

namespace Sitecore.Feature.Business.Controllers
{
    public class MainNavigationController : Controller
    {
        private readonly IMainNavigationBuilder _builder;

        public MainNavigationController(IMainNavigationBuilder builder)
        {
            _builder = builder;
        }

        public MainNavigationController() : this(new MainNavigationBuilder()) { }

        // GET: MainNavigation
        public ActionResult Index()
        {
            var home = Context.Database.GetItem(Context.Site.StartPath);
            return PartialView("~/Views/MainNavigation/Index.cshtml", _builder.Build(home));
        }
    }
}