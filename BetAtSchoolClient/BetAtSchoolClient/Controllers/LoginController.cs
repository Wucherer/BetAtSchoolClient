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
            HttpContext.Session.Add("error", "");
            UserGuide ug = ch.getUser("SuperOverUser", "SuperOverUser");
            return View(ug);
        }

        public ActionResult returnUser(string username, string password)
        {

            UserGuide u = ch.getUser(username, password);

            HttpContext.Session.Add("currentGuide", u);
            
            string s = null;
            try
            {

                if (u != null)
                {
                    if (ch.isAdmin(username))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        if (HttpContext.Session["exception"] != null)
                        {
                            s = "../Login/Index";
                        }
                        else
                        {
                            s = "../User/Index";
                        }
                    }
                }
                else
                {
                    s = "../Login/Index";
                }
            } catch(Exception e)
            {
                HttpContext.Session["error"] = "error in returnUser() - maybe GetUser() - " + "full msg: " + e.Message.ToString();
                return RedirectToAction("Index", "Error");
            }
                
            return View(s, u);
        }
    }
}