using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.Business.Controllers
{
    public class RelatedEventsController : Controller
    {
        private readonly RelatedEventsProvider _provider;

        public RelatedEventsController(RelatedEventsProvider provider)
        {
            _provider = provider;
        }

        public RelatedEventsController() : this(new RelatedEventsProvider()) { }

        // GET: RelatedEvents
        public ActionResult Index()
        {
            return PartialView("~/Views/RelatedEvents/Index.cshtml", _provider.GetRelatedEvents());
        }
    }
}