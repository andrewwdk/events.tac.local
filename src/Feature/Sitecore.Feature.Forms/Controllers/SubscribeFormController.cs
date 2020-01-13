using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAC.Utils.Mvc;
using Sitecore.Feature.Forms.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace Sitecore.Feature.Forms.Controllers
{
    public class SubscribeFormController : Controller
    {
        // GET: SubscribeForm
        //[HttpGet]
        public ActionResult Index()
        {
            var dataSourceId = RenderingContext.Current.Rendering.DataSource;
            var dataSource = Sitecore.Context.Database.GetItem(dataSourceId);
            return PartialView(new SubscribeModel() { 
                ContentHeading = new HtmlString(FieldRenderer.Render(dataSource, "ContentHeading")),
                ContentIntro = new HtmlString(FieldRenderer.Render(dataSource, "ContentIntro"))
            });
        }

        [HttpPost]
        [ValidateFormHandler]
        public ActionResult Index(string email)
        {
            return PartialView("~/Views/SubscribeForm/Confirmation.cshtml");
        }
    }
}