﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.NotFoundPage.Controllers
{
    public class NotFoundPageController : Controller
    {
        // GET: NotFoundPage
        public ActionResult Index()
        {
            return View();
        }
    }
}