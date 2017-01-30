using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifesMoments.Controllers
{
    public class ScrapbookController : Controller
    {
        // GET: Scrapbook
        public ActionResult scrapbookHome()
        {
            return View();
        }
    }
}