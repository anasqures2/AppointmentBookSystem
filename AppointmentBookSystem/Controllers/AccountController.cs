using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AppointmentBookSystem.Controllers
{
    public class AccountController : Controller
    {
        AppointmentBookSystemEntities db = new AppointmentBookSystemEntities();
        // GET: Account
        public ActionResult Login()
        {
            var roleList = db.tbl_Role.ToList();
            ViewBag.Role_Id = new SelectList(roleList, "Role_Id", "Role_Name");
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_User model)
        {
            using (var context =new AppointmentBookSystemEntities())
            {
                bool isValid = context.tbl_User.Any(x => x.User_Username == model.User_Username && x.User_Password == model.User_Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.User_Username, false);
                        return RedirectToAction("Index", "Home");
                  }
                ModelState.AddModelError("", "Invalid Username andpassword");
            }
            var roleList = db.tbl_Role.ToList();
            ViewBag.Role_Id = new SelectList(roleList, "Role_Id", "Role_Name");
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "User_Id,User_Username,User_Password")] tbl_User tbl_User)
        {
            if (ModelState.IsValid)
            {
                tbl_User.User_Role_Id = 1;
                db.tbl_User.Add(tbl_User);
                db.SaveChanges();
                return RedirectToAction("login");
            }
            return View(tbl_User);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}