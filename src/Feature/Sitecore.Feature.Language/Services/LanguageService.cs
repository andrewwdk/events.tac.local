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

            selector.ActiveLanguage = new Models.Language(Context.Language.CultureInfo.DisplayName, Links.LinkManager.GetItemUrl(Sitecore.Context.Item));

            foreach (var siteLanguage in siteLanguages)
            {
                var name = siteLanguage.CultureInfo.DisplayName;
                var currentUrl = Links.LinkManager.GetItemUrl(Sitecore.Context.Item);
                var finalUrl = ChangeUrlLanguage(currentUrl, "/" + Context.Language.Name, "/" + siteLanguage.CultureInfo.Name);
                selector.SupportedLanguages.Add(new Models.Language(name, finalUrl));
            }

            return selector;
        }

        private string ChangeUrlLanguage(string url, string oldLanguage, string newLanguage)
        {
            return url.Replace(oldLanguage, newLanguage);
        }
    }
}