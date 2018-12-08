using BusinessLogic.Repository.Base;
using QuanLyThuVien.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyThuVien.Common;


namespace QuanLyThuVien.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        UserRepository userRepo = new UserRepository();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Login(UserLogin model)
        {
            if(ModelState.IsValid)
            {
                var result = userRepo.checkLogin(model.Username, model.Password);
                if (result == 1)
                {
                    var user = userRepo.GetUserByUsername(model.Username);
                    var loginInfo = new LoginInfor();
                    loginInfo.UserID = user.UserID;
                    loginInfo.Username = user.UserName;
                    Session.Add(Constants.USERSESSION, loginInfo);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị khoá");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return View("Index");
        }

    }
}