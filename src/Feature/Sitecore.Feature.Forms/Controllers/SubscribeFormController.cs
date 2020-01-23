using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAC.Utils.Mvc;
using Sitecore.Feature.Forms.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Outcome.Extensions;
using Sitecore.Analytics;

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
            string identificationSource = "website";
            Sitecore.Analytics.Tracker.Current.Session.Identify(identificationSource, email);
            var contact = Sitecore.Analytics.Tracker.Current.Contact;
            var emails = contact.GetFacet<IContactEmailAddresses>("Emails");
            if (!emails.Entries.Contains("personal"))
            {
                emails.Preferred = "personal";
                var personalEmail = emails.Entries.Create("personal");
                personalEmail.SmtpAddress = email;
            }

            var outcome = new Sitecore.Analytics.Outcome.Model.ContactOutcome(
                Sitecore.Data.ID.NewID,
                new Data.ID("{EC97DE22-55CC-4A65-AACB-50435781D7D6}"),
                new Data.ID(Tracker.Current.Contact.ContactId)
                );
            Tracker.Current.RegisterContactOutcome(outcome);

            var pageEventData = new Sitecore.Analytics.Data.PageEventData("Newsletter Signup")
            {
                Text = "user subsribed."
            };
            Tracker.Current.Interaction.CurrentPage.Register(pageEventData);

            return PartialView("~/Views/SubscribeForm/Confirmation.cshtml");
        }
    }
}