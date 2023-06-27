using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web4.Controllers
{
    public class TestAPIController : Controller
    {
        // GET: TestAPI
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TestAPI()
        {
            return View();
        }
    }
}