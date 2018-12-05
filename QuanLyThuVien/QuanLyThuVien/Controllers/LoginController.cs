using BusinessLogic.Repository.Base;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(UserModelView model)
        {
            if(ModelState.IsValid)
            {
                var usRepos = new UserRepository();
                int result = usRepos.checkLogin(model.UserName, model.Password);
                if (result == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
                }
                else if(result==-1)
                {
                    ModelState.AddModelError( "", "Tài khoản đã bị khoá");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                   
            }
            return View("Index");
        }
    }
}