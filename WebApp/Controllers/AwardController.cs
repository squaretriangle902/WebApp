using WebApp.Common.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Common.Configuration;

namespace WebApp.Controllers
{
    public class AwardController : Controller
    {
        public ActionResult Index()
        {
            var model = AwardModel.GetAllAwards().ToList();
            return View(model);
        }

        public ActionResult Add() 
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Title,Image")]AwardModel awardModel)
        {
            try
            {
                AwardModel.AddAward(awardModel);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Add");
            }
        }

        public ActionResult Update(int awardId)
        {
            var awardModel = AwardModel.GetAward(awardId);
            return View(awardModel);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "Id,Title,Image,ImageId")] AwardModel awardModel)
        {
            AwardModel.UpdateAward(awardModel);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int awardId)
        {
            AwardModel.DeleteAward(awardId);
            return RedirectToAction("Index");
        }

        public ActionResult GetImage(int? imageId)
        {
            var image = AwardModel.GetImage(imageId);
            if (image is null)
            {
                return File(@"C:\Users\squar\source\repos\WebApp\WebApp\Content\defaultAwardImage.png", "image/png");
            }
            return File(image, "image/png");
        }
    }
}