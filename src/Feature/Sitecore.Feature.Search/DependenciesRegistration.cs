using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Feature.Search.Controllers;
using Sitecore.Feature.Search.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Search
{
    public class DependenciesRegistration : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<FacetsController>();
            serviceCollection.AddTransient(typeof(IFacetsService),
                typeof(FacetsService));
        }
    }
}