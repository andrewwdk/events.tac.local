using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Business.Models;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

namespace Sitecore.Feature.Business
{
    public class BreadcrumbBuilder
    {
        private readonly IRenderingContext _context;
        public BreadcrumbBuilder() : this(SitecoreRenderingContext.Create()) { }
        public BreadcrumbBuilder(IRenderingContext context)
        {
            _context = context;
        }

        public IEnumerable<NavigationItem> Build()
        {
            throw new System.NotImplementedException();
        }

    }
}