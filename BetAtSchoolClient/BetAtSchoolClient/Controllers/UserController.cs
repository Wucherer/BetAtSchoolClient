using BetAtSchoolClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetAtSchoolClient.Controllers
{
    public class UserController : Controller
    {
        ControllerHelper ch = new ControllerHelper();
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult getAllStations()
        {
            return Json(ch.getAllStationNames(ch.getAll()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuestionView()
        {
            //---------------------------------------- get Player only 1 time

            Player p = HttpContext.Session["currentPlayer"] as Player;

            if(p.name == null)
            {
                p = new Player();
                HttpContext.Session.Add("currentPlayer", p);
            }

            //---------------------------------------- get Question only 1 time

            string s = HttpContext.Session["currentQuestion"] as string;
            if (s != null)
            {
                HttpContext.Session["currentQuestion"] = (int.Parse(s) + 1).ToString();
            }
            else { HttpContext.Session.Add("currentQuestion", "0"); }

            //---------------------------------------- get Stations only 1 time
            List<Station> l = HttpContext.Session["allStations"] as List<Station>;
            if (l == null) {
                HttpContext.Session.Add("allStations", ch.getAll());
            }

            string currStation = HttpContext.Session["currentStation"] as string;

            return View(ch.getAll());
        }

        public ActionResult setGame(string player, string email)
        {
            string s = null;
            if(ch.checkIfUserExists(player) != true)
            {
                if (email == null) email = "default";
                Player p = new Player(player, 100, email, DateTime.Now.ToShortTimeString()); 
                HttpContext.Session.Add("currentPlayer", p);
                ch.InsertUserInDB(player, email);
                return RedirectToAction("QuestionView", "User");
            } else
            {
                return View("../User/Index");
            }
        }

        public ActionResult setScore(string betAmount, bool isCorrect)
        {
            if(isCorrect == true)
            {
                List<Station> allStations = HttpContext.Session["allStations"] as List<Station>;
                var allQuestionsOfStation = allStations.Where(x => x.StationName == HttpContext.Session["currentStation"] as string).Select(x => x.Questions).ToList();
                decimal q = allQuestionsOfStation[int.Parse(HttpContext.Session["currentQuestion"] as string)].Select(x => x.Quote).FirstOrDefault();
                decimal newScore = decimal.Parse(betAmount) * q;

                ch.setScore((HttpContext.Session["currentPlayer"] as Player).name, newScore);
            } else
            {
                ch.setScore((HttpContext.Session["currentPlayer"] as Player).name, (HttpContext.Session["currentPlayer"] as Player).credit - decimal.Parse(betAmount));
            }
            return RedirectToAction("QuestionView", "User");
        }

        public ActionResult setScoreOffline(string player, string score)
        {
            decimal sc = decimal.Parse(score);
            player = player.Split(':')[1];
            ch.setScore(player, sc);

            return RedirectToAction("Index", "User");
        }

        

        public ActionResult skipQuestion()
        {
            int s = int.Parse(HttpContext.Session["currentQuestion"] as string) + 1;
            HttpContext.Session["currentQuestion"] = s.ToString();
            return RedirectToAction("QuestionView", "User");
        }

        public ActionResult exitQuestion()
        {
            HttpContext.Session["currentQuestion"] = "0";
            return RedirectToAction("Index", "User");

        }
    }
}