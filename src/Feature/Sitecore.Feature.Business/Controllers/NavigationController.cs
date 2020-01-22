using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.Business.Controllers
{
    public class NavigationController : Controller
    {
        private readonly NavigationBuilder _builder;

        public NavigationController(NavigationBuilder builder)
        {
            _builder = builder;
        }

        // GET: Navigation
        public ActionResult Index()
        {
            return PartialView("~/Views/Navigation/Index.cshtml", _builder.Build());
        }
    }
}