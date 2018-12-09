using BusinessLogic.Repository;
using DataProvider.Model;
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
        AuthorRepository authorRepo = new AuthorRepository();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(int page = 1, string searchKey = "")
        {
            var result = authorRepo.GetAllWithPageListSearch(page, 12, searchKey);
            return Json(new { data = result.Select(x => new {
                AuthorID = x.AuthorID,
                AuthorName=x.AuthorName,
                DescriptionAuthor=x.DescriptionAuthor,
                Alias=x.Alias
            }), pageNumber = result.PageCount, keyword = searchKey }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(Author auth)
        {
            try
            {
                authorRepo.Create(auth);
                return Json(new { success = true, message = "Add Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetbyID(int ID)
        {
            var auth = authorRepo.GetById(ID);
            return Json(auth, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Author auth)
        {
            try
            {
                authorRepo.Update(auth);
                return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Delete(int ID)
        {
            try
            {
                var auth = authorRepo.GetById(ID);
                authorRepo.Delete(auth);
                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}