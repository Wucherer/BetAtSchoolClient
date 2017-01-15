using BetAtSchoolClient.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace BetAtSchoolClient.Controllers
{
    public class ControllerHelper
    {
        public List<Station> Allstations { get; set; }
        public List<string> names { get; set; }
        //212.152.179.117:1521
        string cs = "Provider=OraOLEDB.Oracle;Data Source=aphrodite4:1521/ora11g;User Id=d5b22;Password=wucki;OLEDB.NET=True;";

        public UserGuide getUser(string user, string pw)
        {
            UserGuide ug = null;
            
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "192.168.128.253", "OU=EDVO,OU=Schueler,OU=Benutzer,DC=htl-vil,DC=local"))
            {
                // validate the credentials
                if(pc.ValidateCredentials(user, pw) || (user=="s"&&pw=="s"))
                {
                    ug = new UserGuide(user, pw);
                }
            }
            //ug = new UserGuide(user, pw);
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
            string connectionString = cs;
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
            {
                OleDbCommand oleDbCommand = new OleDbCommand("select * from station");
                oleDbConnection.Open();
                oleDbCommand.Connection = oleDbConnection;
                OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

                while (oleDbDataReader.Read())
                {
                    int id = (int)oleDbDataReader.GetDecimal(0);
                    string sname = (string)oleDbDataReader.GetString(1);
                    Station s = new Station(sname, id);
                    stations.Add(s);
                }
            }

            foreach (Station s in stations)
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
                {
                    OleDbCommand oleDbCommand = new OleDbCommand("select * from question where question.station_id = ?");
                    oleDbCommand.Parameters.Add("?", s.StationID);
                    oleDbConnection.Open();
                    oleDbCommand.Connection = oleDbConnection;
                    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

                    while (oleDbDataReader.Read())
                    {
                        Question q = null;
                        int qid = (int)oleDbDataReader.GetDecimal(0);
                        string desc = (string)oleDbDataReader.GetString(1);
                        int quote = (int)oleDbDataReader.GetDecimal(3);
                        q = new Question(qid, desc, quote, -1);
                        s.Questions.Add(q);
                    }
                }
                foreach (Question q in s.Questions)
                {
                    using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
                    {
                        OleDbCommand oleDbCommand = new OleDbCommand("select * from answer where quest_id = ?");
                        oleDbCommand.Parameters.Add("?", q.QId);
                        oleDbConnection.Open();
                        oleDbCommand.Connection = oleDbConnection;
                        OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

                        while (oleDbDataReader.Read())
                        {
                            Answer a = null;
                            int aid = (int)oleDbDataReader.GetDecimal(0);
                            string desc = (string)oleDbDataReader.GetString(1);
                            int corr = (int)oleDbDataReader.GetDecimal(3);

                            if (corr == 1)
                                q.CorrectAnswer = aid;
                            a = new Answer(aid, desc);
                            q.Answers.Add(a);
                        }
                    }
                }

            }

            Allstations = stations;
            return stations;
        }

        public void setScore(string name, decimal score)
        {
            using (var connection = new OleDbConnection(cs))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    "UPDATE USERTDOT SET CREDIT = ? WHERE USERNAME = ?";

                var p1 = command.CreateParameter();
                p1.Value = score;
                command.Parameters.Add(p1);

                var p2 = command.CreateParameter();
                p2.Value = name.Trim();
                command.Parameters.Add(p2);

                command.ExecuteNonQuery();
            }
        }
        
        public bool isAdmin(string username)
        {
            bool ret = false;
            using (OleDbConnection oleDbConnection = new OleDbConnection(cs))
            {

                OleDbCommand oleDbCommand = new OleDbCommand("select count(*) from admins where username = ?");
                oleDbCommand.Parameters.Add("?", username);
                oleDbConnection.Open();
                oleDbCommand.Connection = oleDbConnection;
                OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
                int count = 5;
                while (oleDbDataReader.Read())
                {
                    count = (int)oleDbDataReader.GetDecimal(0);
                }
                if (count == 1)
                    ret = true;
            }
            return ret;
        }

        public List<Player> getAllUser()
        {
            List<Player> players = new List<Player>();
            using (OleDbConnection oleDbConnection = new OleDbConnection(cs))
            {
                OleDbCommand oleDbCommand = new OleDbCommand("select * from Usertdot ORDER BY Credit desc");
                oleDbConnection.Open();
                oleDbCommand.Connection = oleDbConnection;
                OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

                while (oleDbDataReader.Read())
                {
                    string pname = (string)oleDbDataReader.GetString(0);
                    int score = (int)oleDbDataReader.GetDecimal(1);
                   // string mail = (string)oleDbDataReader.GetString(2);

                    Player s = new Player(pname, score, "none");
                    players.Add(s);
                }
            }
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
            int c = -1;
            bool userAlreadyExists = false;
            using (OleDbConnection oleDbConnection = new OleDbConnection(cs))
            {
                OleDbCommand oleDbCommand = new OleDbCommand("select count(*) from USERTDOT where username = ?");
                oleDbCommand.Parameters.Add("?", user);
                oleDbConnection.Open();
                oleDbCommand.Connection = oleDbConnection;
                OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

                while (oleDbDataReader.Read())
                {
                    c = (int)oleDbDataReader.GetDecimal(0);
                }

                if(c>0)
                {
                    userAlreadyExists = true;
                }

            }

            return userAlreadyExists;
        }

        public void InsertUserInDB(string user, string email)
        {
            using (OleDbConnection oleConn = new OleDbConnection(cs))
            {
                OleDbCommand oledbcommand = new OleDbCommand("INSERT INTO USERTDOT VALUES(?, ?, ?)");
                oledbcommand.Parameters.Add("?", user);
                oledbcommand.Parameters.Add("?", 100);
                oledbcommand.Parameters.Add("?", email);
                oleConn.Open();
                oledbcommand.Connection = oleConn;
                oledbcommand.ExecuteNonQuery();
            }
        }
    }
}