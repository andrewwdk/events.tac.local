using Sitecore.Feature.Search.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAC.Utils.Mvc;

namespace Sitecore.Feature.Search.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            var currentPageUrl = System.Web.HttpContext.Current.Request.RawUrl;
            var urlService = new UrlService();
            var placeholderValue = urlService.GetParamValue(currentPageUrl, "search");
            ViewBag.Message = placeholderValue ?? "Search";
            return PartialView();
        }

        [HttpPost]
        [ValidateFormHandler]
        public ActionResult Index(string search)
        {
            var home = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var searchPage = home.GetChildren().First(i => i.Name == "Search Page");
            var pageUrlToNavigate = Sitecore.Links.LinkManager.GetItemUrl(searchPage);
            pageUrlToNavigate += "?" + "search" + "=" + search;

            return Redirect(pageUrlToNavigate);
        }
    }
}