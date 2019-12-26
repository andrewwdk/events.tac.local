using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Feature.Language.Services;

namespace Sitecore.Feature.Language.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ILanguageService _service;

        public LanguageController(ILanguageService service)
        {
            _service = service;
        }
        // GET: Language
        public ActionResult Index()
        {
            var context = Context.Site;
            return PartialView("~/Views/Language/Index.cshtml", _service.Build(context));
        }
    }
}