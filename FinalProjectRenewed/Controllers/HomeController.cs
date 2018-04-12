using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectRenewed.Controllers
{
    public class HomeController : Controller
    {

        Dictionary<int, string> chatQuestions = new Dictionary<int, string>();
        Dictionary<int, string> Answers = new Dictionary<int, string>();

        public void populateChatDic()
        {
            chatQuestions.Add(0, "Ok! That was all for today!");
            chatQuestions.Add(1, "Hey!This is your bot!I will conduct your test!");
            chatQuestions.Add(2, "Whats your qualification?");
            chatQuestions.Add(3, "Ok! That was all for today!");
        }
        public ActionResult Index()
        {
            populateChatDic();
            return View();
        }
        [HttpGet]
        public ActionResult Chat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Chat(string answer, int a)
        {
            if (answer != null && answer != "" && a != 1)
            {
                Answers.Add(a, answer);
                a++;
            }

            if (a == 1)
            {
                return Json(new { message = chatQuestions[a], key = 2 });
            }
            return Json(new { message = chatQuestions[a], key = a });


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}