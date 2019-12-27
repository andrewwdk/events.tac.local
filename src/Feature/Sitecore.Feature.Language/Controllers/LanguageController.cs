using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Feature.Language.Models;
using Sitecore.Sites;
using Sitecore.Feature.Language.Services;

namespace Sitecore.Feature.Language.Controllers
{
    public class LanguageController : Controller
    {
        private readonly IUrlService _service;

        public LanguageController(IUrlService service)
        {
            _service = service;
        }
        // GET: Language
        public ActionResult Index()
        {
            var selector = new LanguageSelector();
            var siteLanguages = Context.Site.Database.GetLanguages();
            var currentPageUrl = System.Web.HttpContext.Current.Request.RawUrl;
             
            selector.ActiveLanguage = new Models.Language(Context.Language.CultureInfo.DisplayName, Links.LinkManager.GetItemUrl(Context.Item));
 
            foreach (var siteLanguage in siteLanguages)
            {
                var name = siteLanguage.CultureInfo.DisplayName;
                var currentUrl = Links.LinkManager.GetItemUrl(Sitecore.Context.Item);
                var finalUrl = currentUrl + "?" + _service.ReplaceQueryStringParam(currentPageUrl, "sc_lang", siteLanguage.CultureInfo.Name);
                selector.SupportedLanguages.Add(new Models.Language(name, finalUrl));
            }

            return PartialView("~/Views/Language/Index.cshtml", selector);
        }
    }
}