using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Areas.Importer.Models;
using Sitecore.Data;
using Sitecore.SecurityModel;
using Sitecore.Data.Items;

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
            IEnumerable<Event> events = null;
            string message = null;
            using(var reader = new System.IO.StreamReader(file.InputStream))
            {
                var contents = reader.ReadToEnd();
                try
                {
                    events = JsonConvert.DeserializeObject<IEnumerable<Event>>(contents);

                    var database = Sitecore.Configuration.Factory.GetDatabase("master");
                    var parent = database.GetItem(parentPath);
                    var templateID = new TemplateID(new ID("{7C3584C1-C656-45E1-9CC7-CC8120E00781}"));

                    using(new SecurityDisabler())
                    {
                        foreach(var currentEvent in events)
                        {
                            var name = ItemUtil.ProposeValidItemName(currentEvent.ContentHeading);
                            Item item = parent.Add(name, templateID);
                            item.Editing.BeginEdit();
                            item["ContentHeading"] = currentEvent.ContentHeading;
                            item["ContentIntro"] = currentEvent.ContentIntro;
                            item["Difficulty"] = currentEvent.Difficulty.ToString();
                            item["Duration"] = currentEvent.Duration.ToString();
                            item["Highlights"] = currentEvent.Highlights;
                            item["StartDate"] = currentEvent.StartDate.ToString();
                            item.Editing.EndEdit();
                        }
                    }
                }
                catch(Exception ex)
                {
                    //to do
                }
            }
            return View();
        }
    }
}