using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Business.Models;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

namespace Sitecore.Feature.Business
{
    public class MetaNavigationBuilder
    {
        private readonly IRenderingContext _context;
        public MetaNavigationBuilder() : this(SitecoreRenderingContext.Create()) { }
        public MetaNavigationBuilder(IRenderingContext context)
        {
            _context = context;
        }

        public IItem Build()
        {
            return _context?.HomeItem;
        }
    }
}