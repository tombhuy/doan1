using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //TypeUserRepository tu = new TypeUserRepository();
            //return View(tu.GetById(1));
            return View();
        }
    }
}