using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Language.Services;
using Sitecore.Feature.Language.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;


namespace Sitecore.Feature.Language
{
    public class DependenciesRegistration : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<LanguageController>();
            serviceCollection.AddSingleton(typeof(ILanguageService), typeof(LanguageService));
        }
    }
}