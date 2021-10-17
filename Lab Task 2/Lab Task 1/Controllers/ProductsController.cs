using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Lab_Task_1.Models.Entity;
using Lab_Task_1.Models.Tables;

namespace Lab_Task_1.Controllers
{
    public class ProductsController : Controller
    {
        [Authorize]
        public ActionResult showProducts()
        {
            products p = new products();
            return View(p.getAllProducts());
        }
        [HttpPost]
        public ActionResult showProducts(string uname, string pass)
        {
            users newu = new users();
            if (newu.isAdmin(uname, pass))
            {
                FormsAuthentication.SetAuthCookie(uname, true);
                return RedirectToAction("helloAdmin");
            }
            if (newu.isUser(uname, pass))
            {
                Session["current_user"] = newu.user_object(uname, pass).uid;
                products p = new products();
                FormsAuthentication.SetAuthCookie(uname, true);
                return View(p.getAllProducts());
            }
            else return RedirectToAction("/../Home/login");
        }
        [Authorize]
        public ActionResult helloAdmin()
        {
            orders allo = new orders();
            return View(allo.getAllOrder());
        }
        public ActionResult ShowSelection()
        {
            if (Session["a"] == null)
            {
                return RedirectToAction("showProducts");
            }
            string json = Session["a"].ToString();
            var d = new JavaScriptSerializer().Deserialize<List<product>>(json);
            return View(d);
        }
        [HttpGet]
        public ActionResult add_to_cart()
        {
            List<product> items = new List<product>();
            string selected = Request["item"];
            string price = Request["price"];
            product p = new product()
            {
                name = selected,
                price = Convert.ToDouble(price)
            };
            if (Session["a"] == null)
            {
                items.Add(p);
                string json = new JavaScriptSerializer().Serialize(items);
                Session["a"] = json;
                string ab = Session["a"].ToString();
            }
            else
            {
                string json = Session["a"].ToString();
                var d = new
                JavaScriptSerializer().Deserialize<List<product>>(json);
                d.Add(p);
                json = new JavaScriptSerializer().Serialize(d);
                Session["a"] = json;
            }
            return RedirectToAction("showProducts");
        }
        public ActionResult confirm_order()
        {
            string json = Session["a"].ToString();
            var d = new JavaScriptSerializer().Deserialize<List<product>>(json);
            orders od = new orders();
            int userID = Convert.ToInt32(Session["current_user"]);
            foreach(var i in d)
            {
                od.addOrder(i, userID);
            }
            return View();
        }
        public ActionResult processProduct()
        {
            int id = Convert.ToInt32(Request["id"]);
            orders od = new orders();
            od.setProcessed(id);
            return RedirectToAction("helloAdmin");
        }
        public ActionResult cancelProduct()
        {
            int id = Convert.ToInt32(Request["id"]);
            orders od = new orders();
            od.setCancel(id);
            return RedirectToAction("helloAdmin");
        }
    }
}