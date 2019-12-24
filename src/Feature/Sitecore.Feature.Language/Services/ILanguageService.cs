using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Language.Models;
using Sitecore.Sites;

namespace Sitecore.Feature.Language.Services
{
    public interface ILanguageService
    {
        LanguageSelector Build(SiteContext context);
    }
}