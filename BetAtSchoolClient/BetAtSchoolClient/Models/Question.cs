using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetAtSchoolClient.Models
{
    public class Question
    {

        public int QId { get; set; }
        public string Description { get; set;}
        public decimal Quote { get; set; }
        public List<Answer> Answers { get; set; }
        public int CorrectAnswer { get; set; }

        public Question() { }

        public Question(int qid, string desc, int quote, int corr) {
            QId = qid;
            Description = desc;
            Quote = quote;
            CorrectAnswer = CorrectAnswer;
            Answers = new List<Answer>();
        }

    }
}