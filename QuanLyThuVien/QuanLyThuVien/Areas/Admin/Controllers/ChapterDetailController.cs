using BusinessLogic.Repository;
using DataProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    public class ChapterDetailController : Controller
    {
        // GET: Admin/ChapterDetail
        ChapterDetailRepository chapterRepo = new ChapterDetailRepository();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(int page = 1, string searchKey = "")
        {
            var result = chapterRepo.GetAllWithPageListSearch(page, 12, searchKey);
            return Json(new { data = result.Select(x => new {
                IDBook=x.IDBook,
                ChapterID=x.ChapterID,
                Alias=x.Alias,
                NameChapter=x.NameChapter,
                Content=x.Content
            }), pageNumber = result.PageCount, keyword = searchKey }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Add(ChapterDetail chapter)
        {
            if(ModelState.IsValid)
            {

            }
            return View();
        }


        [HttpPost]
        public JsonResult Delete(int bookid,int chapterid)
        {
            try
            {
                var chapter = chapterRepo.getByID2(bookid,chapterid);
                chapterRepo.Delete(chapter);
                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}