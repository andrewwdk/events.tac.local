using Sitecore.Feature.Business.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.Business.Controllers
{
    public class RelatedEventsController : Controller
    {
        private readonly IRelatedEventsProvider _provider;

        public RelatedEventsController(IRelatedEventsProvider provider)
        {
            _provider = provider;
        }


        // GET: RelatedEvents
        public ActionResult Index()
        {
            return PartialView("~/Views/RelatedEvents/Index.cshtml", _provider.GetRelatedEvents());
        }
    }
}