using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        public ActionResult Get(int userId)
        {
            var model = UserWithAwardsModel.GetUser(userId);
            return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "Id,Image,Name,BirthDate,Age")]UserModel userModel)
        {
            try
            {
                UserModel.Add(userModel);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Add");
            }
        }

        public ActionResult Edit(int userId)
        {
            var model = UserModel.GetUser(userId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserModel userModel)
        {
            UserModel.Edit(userModel);
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int userId)
        {
            UserModel.Remove(userId);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveAward(int userId, int awardId)
        {
            UserWithAwardsModel.RemoveAward(userId, awardId);
            return RedirectToAction("GetAvailableAwards", new {userId = userId});
        }

        public ActionResult AddAward(int userId, int awardId)
        {
            UserWithAwardsModel.AddAward(userId, awardId);
            return RedirectToAction("GetAvailableAwards", new { userId = userId });
        }

        public ActionResult GetAvailableAwards(int userId)
        {
            var model = new AddAwardsModel(userId);
            return View(model);
        }

        public ActionResult GetImage(int userId) 
        {
            var image = UserModel.GetImage(userId);
            if (image is null)
            {
                return File("~/Content/deafultUserImage.png", "image/png");
            }
            return File(image, "image/png");
        }
    }
}