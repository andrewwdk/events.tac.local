using Sitecore.Feature.Search.Models;
using Sitecore.Feature.Search.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAC.Utils.Mvc;

namespace Sitecore.Feature.Search.Controllers
{
    public class EventsListController : Controller
    {
        private readonly EventsProvider _provider;

        public EventsListController() : this(new EventsProvider()) { }
        public EventsListController(EventsProvider provider)
        {
            _provider = provider;
        }
        // GET: EventsList
        public ActionResult Index(int page = 1)
        {
            return PartialView(_provider.GetEventsList(page - 1));
        }

        public ActionResult Search(int page = 1)
        {
            var currentPageUrl = System.Web.HttpContext.Current.Request.RawUrl;
            var urlService = new UrlService();
            var search = urlService.GetParamValue(currentPageUrl, "search");
            var checkedDurations = urlService.GetParamValue(System.Web.HttpContext.Current.Request.RawUrl, "duration")?.Split(',');
            var checkedDifficulties = urlService.GetParamValue(System.Web.HttpContext.Current.Request.RawUrl, "difficulty")?.Split(',');
            var durations = checkedDurations != null ? Array.ConvertAll(checkedDurations, s => int.Parse(s)) : null;
            var difficultiess = checkedDifficulties != null ? Array.ConvertAll(checkedDifficulties, s => int.Parse(s)) : null;
            return PartialView("~/Views/EventsList/Index.cshtml", _provider.GetEventsListBySearch(page - 1, search, durations, difficultiess));
        }
    }
}