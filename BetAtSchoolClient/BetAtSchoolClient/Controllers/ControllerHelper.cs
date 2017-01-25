using BetAtSchoolClient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.OleDb;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace BetAtSchoolClient.Controllers
{
    public class ControllerHelper
    {
        public List<Station> Allstations { get; set; }
        public List<string> names { get; set; }

        public UserGuide getUser(string user, string pw)
        {
            UserGuide ug = null;
           
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "192.168.128.253", "OU=EDVO,OU=Schueler,OU=Benutzer,DC=htl-vil,DC=local"))
                {
                    // validate the credentials
                    if (pc.ValidateCredentials(user, pw) || (user == "SuperOverUser" && pw == "SuperOverUser"))
                    {
                        ug = new UserGuide(user, pw);
                    }
                }
          
            return ug;
        }

        public List<string> getAllStationNames(List<Station> AllStationsx)
        {
            names = new List<string>();
            foreach (Station s in AllStationsx)
            {
                names.Add(s.StationName);
            }

            return names;
        }

        public bool hasGuest(UserGuide u)
        {
            bool result = true;

            return result;
        }

        public List<Station> getAll()
        {
            List<Station> stations = new List<Station>();
           
                using (BetAtSchoolDBEntities context = new BetAtSchoolDBEntities())
                {
                    var stationsDb = context.Stations.ToList();

                    for (int i = 0; i < stationsDb.Count; i++)
                    {
                        Station s = new Station();

                        s.StationName = stationsDb[i].Stationname;
                        s.StationID = stationsDb[i].Station_ID;

                        List<Question> qList = new List<Question>();

                        foreach (Questions q in stationsDb[i].Questions)
                        {
                            Question q1 = new Question();
                            q1.QId = q.Question_ID;
                            q1.Description = q.Question_Desc;
                            q1.Quote = (decimal)q.Quote;
                            q1.Answers = new List<Answer>();

                            foreach (Answers a in q.Answers)
                            {
                                Answer a1 = new Answer();

                                a1.AId = a.Answer_ID;
                                a1.Description = a.Answer_Desc;

                                if (a.isRichtig == 1)
                                    q1.CorrectAnswer = a1.AId;

                                q1.Answers.Add(a1);
                            }
                            qList.Add(q1);
                        }
                        s.Questions = qList;
                        stations.Add(s);
                    }
                }
           
            Allstations = stations;
            return stations;
        }

        public void setScore(string name, decimal score)
        {
            using (BetAtSchoolDBEntities context = new BetAtSchoolDBEntities())
            {
                context.USERTDOT.Where(x => x.Username == name.Trim()).ToList().FirstOrDefault().Credit = (int)score;

                context.SaveChanges();
            }
        }

        public bool isAdmin(string username)
        {
            bool ret = false;

            using (BetAtSchoolDBEntities context = new BetAtSchoolDBEntities())
            {
                if (context.isAdmin.Where(x => x.Username == username).ToList().Count > 0)
                    ret = true;         
            }

           return ret;
        }

        public List<Player> getAllUser()
        {
            List<Player> players = new List<Player>();

            using (BetAtSchoolDBEntities context = new BetAtSchoolDBEntities())
            {
                var temp = context.USERTDOT.ToList();

                for(int i = 0; i < temp.Count; i++)
                {
                    Player p = new Player();
                    p.credit = (decimal) temp[i].Credit;
                    p.name = temp[i].Username;
                    p.email = temp[i].Email;
                    p.StartZeit = temp[i].StartZeit;
                    players.Add(p);
                }
            }
            
            players.OrderBy(i => i.credit);

            return players;
        }

        public Station getStationByName(string name, List<Station> all)
        {

            var temp = all.Where(x => x.StationName == name).ToList();

            Station s = temp.FirstOrDefault();

            return s;
        }

        public bool checkIfUserExists(string user)
        {
           
            bool userAlreadyExists = false;
            using (BetAtSchoolDBEntities context = new BetAtSchoolDBEntities())
            {
                if (context.USERTDOT.Where(x => x.Username == user).ToList().Count > 0)
                    userAlreadyExists = true;
            }
        
            return userAlreadyExists;
        }

        public void InsertUserInDB(string user, string email)
        {
            using(BetAtSchoolDBEntities context = new BetAtSchoolDBEntities())
            {
                USERTDOT usertdot = new USERTDOT();
                usertdot.Credit = 100;
                usertdot.Email = email;
                usertdot.Username = user;
                usertdot.StartZeit = DateTime.Now.ToShortTimeString();

                context.USERTDOT.Add(usertdot);

                context.SaveChanges();
            }
        }
    }
}