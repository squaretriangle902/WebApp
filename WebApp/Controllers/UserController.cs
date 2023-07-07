using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var model = UserModel.GetAll().ToList();
            return View(model);
        }

        public ActionResult Get(int id)
        {
            var model = UserWithAwardsModel.GetUser(id);
            return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserModel userModel)
        {
            UserModel.Add(userModel);
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            UserModel.Remove(id);
            return RedirectToAction("Index");
        }
    }
}