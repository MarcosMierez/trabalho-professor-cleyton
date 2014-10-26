using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Palestra.Controllers
{
    public class PalestrasController : Controller
    {
        // GET: Palestra
        public ActionResult Index()
        {
            return View();
        }
    }
}