using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectRenewed.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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


            //  if (a == 88)
            // {
            //     a = 0;
            //     Session["q"] = a;
            // }
            // else
            // {
            //     a = (int)Session["q"] + 1;
            //     Session["q"] = a;
            // }


            string[] questions =
            {
                "Hey! This is your bot! I will conduct your test!",
                "Whats Your Name?",
                "How Old are you?",
                "Whats your qualification?",
                "Ok! That was all for today!"
            };


            return Json(new { message = questions[(int)a] });


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}