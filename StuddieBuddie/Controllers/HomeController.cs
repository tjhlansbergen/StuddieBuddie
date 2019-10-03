using StuddieBuddie.Code;
using StuddieBuddie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StuddieBuddie.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", new IndexModel());
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            //store file
            Test test = DataStore.StoreTest(file);

            //check result
            if (test == null) return View("Message", new MessageModel(Common.UPLOAD_FAIL));

            //and show guid
            return View("Message", new MessageModel(Common.UPLOAD_DONE, string.Format("Name: {0}", test.Title), string.Format("Questions: {0}", test.Questions.Count.ToString())));
        }

        public ActionResult DeleteTest(string guid)
        {
            //try to delete
            DataStore.DeleteTest(guid);

            //and refresh
            return View("Index", new IndexModel());
        }

        public ActionResult FirstQuestion(string guid)
        {
            //check if guid is valid, otherwise show message
            if (!DataStore.TestExists(guid))
                return View("Message", new MessageModel(Common.NO_TEST_MSG));

            //get test
            Test test = DataStore.GetTest(guid);

            //scramble questions
            test.Questions = test.Questions.OrderBy(a => Guid.NewGuid()).ToList();

            //set question numbers
            for (int i = 0; i < test.Questions.Count; i++)
            {
                test.Questions[i].TestTitle = test.Title;
                test.Questions[i].TestQuestions = test.Questions.Count;
                test.Questions[i].QuestionNumber = i + 1;
            }

            //store test and score for session
            Session["test"] = test;
            Session["score"] = 0;

            //and show
            return View("Question", test.Questions[0]);
        }

        public ActionResult Question(bool repeat_question)
        {
            //check if we have a session
            if (Session["test"] == null)
                return View("Message", new MessageModel(Common.NO_SESS_MSG));

            //get test from session
            Test test = (Test)Session["test"];
            int score = (int)Session["score"];

            //set score
            if(repeat_question) score++;
            Session["score"] = score;

            //move to next question
            //test.Questions.Add(test.Questions[0]);
            if(!repeat_question) test.Questions.RemoveAt(0);

            //check if there are questions left
            if (test.Questions.Count == 0)
                return View("Message", new MessageModel(Common.TS_DONE_MSG, string.Format("{0} {1}", Common.TS_RETR_MSG, Session["score"])));

            //scramble answers
            test.Questions[0].Answers = test.Questions[0].Answers.OrderBy(a => Guid.NewGuid()).ToList();

            //and show
            return View("Question", test.Questions[0]);
        }

        public ActionResult Answer(bool correct)
        {
            //check if we have a session
            if (Session["test"] == null)
                return View("Message", new MessageModel(Common.NO_SESS_MSG));

            //get test from session
            Test test = (Test)Session["test"];

            //show result
            return View("Answer", new AnswerModel(correct, test.Questions[0]));
        }

        public FilePathResult DownloadSample()
        {
            FilePathResult fpr = new FilePathResult("~/Sample/Sample.json", "application/json")
            {
                FileDownloadName = "Sample.json"
            };

            return fpr;
        }
    }
}