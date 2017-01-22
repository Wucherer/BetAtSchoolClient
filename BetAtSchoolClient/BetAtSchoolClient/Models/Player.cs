using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetAtSchoolClient.Models
{
    public class Player
    {
        public string name { get; set; }
        public decimal credit { get; set; }
        public string email { get; set; }
        public string StartZeit { get; set; }

        public Player(string _name,decimal _credit, string _email, string _time)
        {
            name = _name;
            credit = _credit;
            email = _email;
            StartZeit = _time;
        }

        public Player()
        {
            name = null;
            credit = 0;
        }
    }
}