using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StuddieBuddie.Code
{
    public class Test
    {
        public List<Question> Questions { get; set; }
        public string Title { get; set; }
    }

    public class Question
    {
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }

        public string TestTitle { get; set; } = "";
        public int QuestionNumber { get; set; } = 0;
        public int TestQuestions { get; set; } = 0;

        public Question()
        {
            Answers = new List<Answer>();
        }
    }

    public struct Answer
    {
        public string Text { get; set; }
        public bool Correct { get; set; }
    }
}