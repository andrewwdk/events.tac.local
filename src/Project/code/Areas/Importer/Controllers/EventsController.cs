using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Areas.Importer.Models;
using Sitecore.Data;
using events.tac.local.Areas.Importer.Services;
using System.Text;

namespace events.tac.local.Areas.Importer.Controllers
{
    public class EventsController : Controller
    {
        // GET: Importer/Events
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string parentPath)
        {
            EventsService service = new EventsService();

            using(var reader = new System.IO.StreamReader(file.InputStream))
            {
                var contents = reader.ReadToEnd();
                try
                {
                    IEnumerable<Event> events = JsonConvert.DeserializeObject<IEnumerable<Event>>(contents);

                    var database = Sitecore.Configuration.Factory.GetDatabase("master");
                    var parent = database.GetItem(parentPath);
                    var templateID = new TemplateID(new ID("{7C3584C1-C656-45E1-9CC7-CC8120E00781}"));

                    service.AddItems(parent, events, templateID);
                }
                catch(Exception ex)
                {
                    //to do
                }
            }

            ViewBag.Message = string.Format("{0} updated, {1} added.", service.UpdateCount, service.AddCount);
            return View();
        }
    }
}