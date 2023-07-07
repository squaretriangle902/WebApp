﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            var model = NoteModel.GetAll().ToList();
            return View(model);
        }

        public ActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(NoteModel noteModel) 
        {
            NoteModel.Add(noteModel);
            return RedirectToAction("Index");
        }
    }
}