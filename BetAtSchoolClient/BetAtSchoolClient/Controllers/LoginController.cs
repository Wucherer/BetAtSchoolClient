using BetAtSchoolClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetAtSchoolClient.Controllers
{
    public class LoginController : Controller
    {
        ControllerHelper ch = new ControllerHelper();

        // GET: Login
        public ActionResult Index()
        {
            UserGuide ug = ch.getUser("s", "s");
            return View(ug);
        }

        public ActionResult returnUser(string username, string password)
        {
            UserGuide u = ch.getUser(username, password);

            HttpContext.Session.Add("currentGuide", u);
            
            string s = null;
          
            
                if(u != null)
                {
                    if (ch.isAdmin(username))
                    {
                        return RedirectToAction("Index", "Admin");
                    } else {
                        s = "../User/Index";
                    }
                }
                else
                {
                    s = "../Login/Index";
                }
            

            return View(s, u);
        }
    }
}