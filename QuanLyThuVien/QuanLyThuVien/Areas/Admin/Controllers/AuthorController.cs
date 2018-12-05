using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Admin/Author
        public ActionResult Index()
        {
            var auRepty = new AuthorRepository();
            var result = auRepty.GetById(1);
            return View(result);
        }

        public ActionResult Detail()
        {
            var auRepty = new AuthorRepository();
            var result = auRepty.GetById(1);
            return View(result);
        }

        public ActionResult ListAuthor()
        {
            var auRepty = new AuthorRepository();
            var result = auRepty.GetAll();
            return View(result);
        }


        public ActionResult Create()
        {
            return View();
        }
    }
}