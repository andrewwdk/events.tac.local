using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Business.Builders;
using Sitecore.Feature.Business.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Sitecore.Feature.Business
{
    public class DependenciesRegistration : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MetaNavigationController>();
            serviceCollection.AddTransient(typeof(IMetaNavigationBuilder),
                typeof(MetaNavigationBuilder));
            serviceCollection.AddTransient<MainNavigationController>();
            serviceCollection.AddTransient(typeof(IMainNavigationBuilder),
                typeof(MainNavigationBuilder));
            serviceCollection.AddTransient<NavigationBuilder>();
            serviceCollection.AddTransient<NavigationController>();
        }
    }
}