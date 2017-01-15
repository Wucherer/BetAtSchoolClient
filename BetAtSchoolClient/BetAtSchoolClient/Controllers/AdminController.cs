using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetAtSchoolClient.Controllers
{
    public class AdminController : Controller
    {
        ControllerHelper ch = new ControllerHelper();

        // GET: Admin
        public ActionResult Index()
        {
            return View(ch.getAllUser());
        }
    }
}