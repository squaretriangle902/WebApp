using Denis.UserList.Common.Entities;
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

namespace WebApp.Controllers
{
    public class AwardController : Controller
    {
        public ActionResult Index()
        {
            var model = AwardModel.GetAll().ToList();
            return View(model);
        }

        public ActionResult Add() 
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Title,Image")]AwardModel awardModel)
        {
            AwardModel.Add(awardModel);
            return RedirectToAction("Index");
        }

        public ActionResult GetImage(int awardId)
        {
            return File(AwardModel.GetImage(awardId), "image/png");
        }
    }
}