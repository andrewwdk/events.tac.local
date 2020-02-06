using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Pipelines.HttpRequest;

namespace Sitecore.Feature.NotFoundPage
{
    public class NotFoundProcessor : Sitecore.Pipelines.HttpRequest.HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (Sitecore.Context.Item != null || Sitecore.Context.Site == null || Sitecore.Context.Database == null)
            { 
                return;
            }

            var database = Sitecore.Context.Database;
            var home = database.GetItem("/sitecore/content/events/home");
            var notFoundPage = home?.GetChildren().First(x => x.TemplateID.ToString() == "{051FF4CA-456A-49A3-9840-C5DD919B4D1D}");
            Sitecore.Context.Item = notFoundPage;
        }
    }
}