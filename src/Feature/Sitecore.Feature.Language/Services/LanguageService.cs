using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Language.Models;
using Sitecore.Sites;
using Sitecore.Web;

namespace Sitecore.Feature.Language.Services
{
    public class LanguageService : ILanguageService
    {
        public LanguageSelector Build(SiteContext context)
        {
            var selector = new LanguageSelector();
            var siteLanguages = context.Database.GetLanguages();

            foreach(var siteLanguage in siteLanguages)
            {
                var name = siteLanguage.CultureInfo.Name;
                var url = "http://" + GetSite(siteLanguage.Name).HostName;
                selector.SupportedLanguages.Add(new Models.Language(name, url));
            }

            selector.ActiveLanguage = new Models.Language(context.Language, null);

            return selector;
        }

        private SiteInfo GetSite(string language)
        {
            return SiteContextFactory.Sites.First(s => s.Language == language && s.HostName != string.Empty);
        }
    }
}