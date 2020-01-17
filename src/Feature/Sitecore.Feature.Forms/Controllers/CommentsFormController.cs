using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAC.Utils.Mvc;

namespace Sitecore.Feature.Forms.Controllers
{
    public class CommentsFormController : Controller
    {
        // GET: CommentsForm
        //[HttpGet]
        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateFormHandler]
        public ActionResult Comment(string comment, string name)
        {
            return PartialView("~/Views/CommentsForm/Confirmation.cshtml");
        }
    }
}