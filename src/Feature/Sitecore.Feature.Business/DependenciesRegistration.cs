using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Business.Builders;
using Sitecore.Feature.Business.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

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

            
            serviceCollection.AddTransient<NavigationController>();
            serviceCollection.AddTransient(typeof(INavigationBuilder),
                typeof(NavigationBuilder));
            serviceCollection.AddTransient(typeof(IRenderingContext),
                typeof(SitecoreRenderingContext));

            serviceCollection.AddTransient<BreadcrumbController>();
            serviceCollection.AddTransient(typeof(IBreadcrumbBuilder),
                typeof(BreadcrumbBuilder));

            serviceCollection.AddTransient<RelatedEventsController>();
            serviceCollection.AddTransient(typeof(IRelatedEventsProvider),
                typeof(RelatedEventsProvider));
        }
    }
}