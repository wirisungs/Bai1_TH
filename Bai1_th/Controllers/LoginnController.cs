﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bai1_th.Controllers
{
    public class LoginController : Controller
    {
        DBQLCLOTHEntities1 database = new DBQLCLOTHEntities1();

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AuthenLogin(User _user)
        {
            try
            {
                var check_User = database.Users
                    .Where(s => s.UserName == _user.UserName && s.PassWord == _user.PassWord)
                    .FirstOrDefault();

                if (check_User == null)
                {
                    ViewBag.ErrorLogin = "Invalid credentials";
                    return View("Login");
                }
                else
                {
                    Session["userName"] = _user.UserName;

                    return RedirectToAction("Home", "Login");
                }
            }
            catch
            {
                return View("Login");
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult AuthenRegister(User _user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    database.Users.Add(_user);
                    database.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    // Trả về view với thông tin lỗi nếu có
                    return View("Register", _user);
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception (nếu cần thiết)
                return View("Register", _user);
            }
        }




        public ActionResult Home()
        {
            return View();
        }

        public ActionResult AdminORUser()
        {
            return View();
        }


    }
}