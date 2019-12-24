using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Mvc.Presentation;
using Sitecore.Feature.Business.Builders;

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
            var dataSourceId = RenderingContext.CurrentOrNull.Rendering.DataSource;
            var dataSource = Sitecore.Context.Database.GetItem(dataSourceId);
            return PartialView("~/Views/MetaNavigation/Index.cshtml", _builder.Build(dataSource));
        }
    }
}