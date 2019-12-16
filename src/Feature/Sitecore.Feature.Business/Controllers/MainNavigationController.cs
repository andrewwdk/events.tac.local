using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.Business.Controllers
{
    public class MainNavigationController : Controller
    {
        private readonly MainNavigationBuilder _builder;

        public MainNavigationController(MainNavigationBuilder builder)
        {
            _builder = builder;
        }

        public MainNavigationController() : this(new MainNavigationBuilder()) { }

        // GET: MainNavigation
        public ActionResult Index()
        {
            return PartialView("~/Views/MainNavigation/Index.cshtml", _builder.Build());
        }
    }
}