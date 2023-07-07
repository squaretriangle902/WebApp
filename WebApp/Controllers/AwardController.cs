using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AwardController : Controller
    {
        // GET: Award
        public ActionResult Index()
        {
            var model = AwardModel.GetAll().ToList();
            return View(model);
        }

        public ActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AwardModel awardModel)
        {
            AwardModel.Add(awardModel);
            return RedirectToAction("Index");
        }
    }
}