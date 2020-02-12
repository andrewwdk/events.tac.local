using Sitecore.ContentSearch;
using Sitecore.Feature.Search.Models;
using Sitecore.Feature.Search.Services;
using Sitecore.Mvc.Presentation;
using System.Web.Mvc;
using TAC.Utils.Mvc;

namespace Sitecore.Feature.Search.Controllers
{
    public class FacetsController : Controller
    {
        private readonly IFacetsService _service;

        public FacetsController(IFacetsService service)
        {
            _service = service;
        }

        // GET: Facets
        public ActionResult Index()
        {
            var indexname = $"events_{RenderingContext.Current.ContextItem.Database.Name.ToLower()}_index";
            var index = ContentSearchManager.GetIndex(indexname);
            var urlService = new UrlService();
            var checkedDurations = urlService.GetParamValue(System.Web.HttpContext.Current.Request.RawUrl, "duration");
            var checkedDifficulties = urlService.GetParamValue(System.Web.HttpContext.Current.Request.RawUrl, "difficulty");
            var search = urlService.GetParamValue(System.Web.HttpContext.Current.Request.RawUrl, "search");
            var model = _service.GetFacets(index, search);
            model.Mark(checkedDurations?.Split(','), checkedDifficulties?.Split(','));
            return PartialView(model);
        }

        [HttpPost]
        [ValidateFormHandler]
        public ActionResult Index(Facets model)
        {
            var urlService = new UrlService();
            var currentPageUrl = urlService.RemoveParams(System.Web.HttpContext.Current.Request.RawUrl, "duration", "difficulty");

            foreach(var facet in model.DurationFacets)
            {
                if (facet.IsChecked)
                {
                    currentPageUrl = urlService.SetParamAndValue(currentPageUrl, "duration", facet.Name);
                }
            }

            foreach (var facet in model.DifficultyFacets)
            {
                if (facet.IsChecked)
                {
                    currentPageUrl = urlService.SetParamAndValue(currentPageUrl, "difficulty", facet.Name);
                }
            }
            return Redirect(currentPageUrl);
        }
    }
}