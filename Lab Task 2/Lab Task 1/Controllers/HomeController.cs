using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab_Task_1.Models.Tables;
using System.Web.Mvc;

namespace Lab_Task_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string uname, string phone, string pass)
        {
            users newu = new users();
            newu.addNewUser(uname, phone, pass);
            return View();
        }
        public ActionResult Register()
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