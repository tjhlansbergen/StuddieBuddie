using StuddieBuddie.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StuddieBuddie.Models
{
    public class AnswerModel
    {
        public bool Correct { get; set; }
        public Question Question { get; set; }

        public AnswerModel(bool correct, Question question)
        {
            Correct = correct;
            Question = question;
        }
    }
}