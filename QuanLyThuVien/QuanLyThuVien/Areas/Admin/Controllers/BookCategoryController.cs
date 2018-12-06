using BusinessLogic.Repository;
using DataProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    public class BookCategoryController : Controller
    {
        // GET: Admin/BookCategory
        BookCategoryRepository bookCateRepo = new BookCategoryRepository();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(int page = 1, string searchKey = "")
        {
            var result = bookCateRepo.GetAllWithPageListSearch(page, 12, searchKey);
            return Json(new { data = result, pageNumber = result.PageCount, keyword = searchKey }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(BookCategory bookcate)
        {
            try
            {
                bookcate.CreatedDate = DateTime.Now;
                bookCateRepo.Create(bookcate);
                return Json(new { success = true, message = "Add Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetbyID(int ID)
        {
            var user = bookCateRepo.GetById(ID);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(BookCategory bookcate)
        {
            try
            {
                bookCateRepo.Update(bookcate);
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
                var bookcate = bookCateRepo.GetById(ID);
                bookCateRepo.Delete(bookcate);
                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}