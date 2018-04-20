﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoutUp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["email"] != null)
                Response.Redirect("/home/newsfeed");
            return View();
        }
        public ActionResult Newsfeed()
        {
            if (Session["email"] == null)
                Response.Redirect("/");
            ViewBag.email = Session["email"];
            ViewBag.id = Session["id"];
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
    
    }
}