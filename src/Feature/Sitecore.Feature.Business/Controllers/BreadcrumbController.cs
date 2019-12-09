using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.Business.Controllers
{
    public class BreadcrumbController : Controller
    {
        private readonly BreadcrumbBuilder _builder;

        public BreadcrumbController(BreadcrumbBuilder builder)
        {
            _builder = builder;
        }

        public BreadcrumbController() : this(new BreadcrumbBuilder()) { }

        // GET: Breadcrumb
        public ActionResult Index()
        {
            return PartialView("~/Views/Breadcrumb/Index.cshtml", _builder.Build());
        }
    }
}