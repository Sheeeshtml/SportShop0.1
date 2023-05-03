using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using SportShop.Models;

namespace SportShop.Controllers
{
    public class AccountController : Controller
    {
        TACV_DBEntities3 entity = new TACV_DBEntities3();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel credentials)
        {
            bool userExist = entity.UsersTb1.Any(x => x.Email == credentials.Email && x.Passcode == credentials.Password);
            UsersTb1 u = entity.UsersTb1.FirstOrDefault(x => x.Email == credentials.Email && x.Passcode == credentials.Password);
            if (userExist)
            {
                FormsAuthentication.SetAuthCookie(u.Username, false); 
                return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError("", "Username or Password is wrong "); 

            return View();
        }
        [HttpPost]
        public ActionResult Signup(UsersTb1 userinfo)
        {
            entity.UsersTb1.Add(userinfo);
            entity.SaveChanges();
            return RedirectToAction("Login");
        }


        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}