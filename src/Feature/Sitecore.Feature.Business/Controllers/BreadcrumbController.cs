using Sitecore.Feature.Business.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.Business.Controllers
{
    public class BreadcrumbController : Controller
    {
        private readonly IBreadcrumbBuilder _builder;

        public BreadcrumbController(IBreadcrumbBuilder builder)
        {
            _builder = builder;
        }

        // GET: Breadcrumb
        public ActionResult Index()
        {
            return PartialView("~/Views/Breadcrumb/Index.cshtml", _builder.Build());
        }
    }
}