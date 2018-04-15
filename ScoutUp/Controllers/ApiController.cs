using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoutUp.Classes;
namespace ScoutUp.Controllers
{
    public class ApiController : Controller
    {
        [HttpPost]
        public ActionResult Login(string email,string password)
        {
           

            if(email=="buraqportakal@hotmail.com" && password=="3507781")
                {
                Session["email"] = email;
                return Json(new LoginResult(1));
            }
            return Json(new LoginResult(0));

        }
    }
}