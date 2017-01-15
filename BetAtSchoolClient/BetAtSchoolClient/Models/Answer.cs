using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetAtSchoolClient.Models
{
    public class Answer
    {
        public int AId { get; set; }
        public string Description { get; set; }

        public Answer(int i, string s)
        {
            AId = i;
            Description = s;
        }
    }
}




