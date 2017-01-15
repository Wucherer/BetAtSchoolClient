using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetAtSchoolClient.Models
{
    public class UserGuide
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserGuide(string user, string pw)
        {
            this.Username = user;
            this.Password = pw;
        }
    }
}