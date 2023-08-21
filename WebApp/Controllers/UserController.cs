using WebApp.Common.Entities;
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
        public ActionResult Index()
        {
            var model = UserModel.GetAllUsers().ToList();
            return View(model);
        }

        public ActionResult Details(int userId)
        {
            var model = UserWithAwardsModel.GetUser(userId);
            return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Image,Name,BirthDate,ImageId,Age")] UserModel userModel)
        {
            try
            {
                UserModel.AddUser(userModel);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Add");
            }
        }

        public ActionResult Update(int userId)
        {
            var model = UserModel.GetUser(userId);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "Id,Image,Name,BirthDate,ImageId,Age")] UserModel userModel)
        {
            UserModel.Update(userModel);
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int userId)
        {
            UserModel.DeleteUser(userId);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveAward(int userId, int awardId)
        {
            UserWithAwardsModel.RemoveAward(userId, awardId);
            return RedirectToAction("GetAvailableAwards", new { userId });
        }

        public ActionResult AddAward(int userId, int awardId)
        {
            UserWithAwardsModel.AddAward(userId, awardId);
            return RedirectToAction("GetAvailableAwards", new { userId });
        }

        public ActionResult GetAvailableAwards(int userId)
        {
            var model = new AddAwardsModel(userId);
            return View(model);
        }

        public ActionResult GetImage(int? imageId) 
        {
            var image = UserModel.GetImage(imageId);
            if (image is null)
            {
                return File(@"C:\Users\squar\source\repos\WebApp\WebApp\Content\defaultUserImage.png", "image/png");
            }
            return File(image, "image/png");
        }
    }
}