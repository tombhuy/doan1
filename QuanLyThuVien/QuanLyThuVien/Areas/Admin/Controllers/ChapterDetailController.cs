using BusinessLogic.Repository;
using DataProvider.Model;
using QuanLyThuVien.Areas.Admin.Models;
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
        public ActionResult Index(int id=0,string namebook="")
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


        public ActionResult GetListBook()
        {
            var listbook = new BookRepository().GetAll().Select(x => new
            {
                BookID = x.BookID,
                BookName = x.BookName
            });
            return Json(listbook, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Add(int idbook)
        {
            LoadData(idbook);
            return View("Add");
        }


        public void LoadData(int idbook)
        {
            var book = new BookRepository().GetById(idbook);
            var lastChapter = new BookRepository().GetAllChapterByIDBook(1, 10000, idbook).Count;
            ViewBag.IDBookM = idbook;
            ViewBag.ChapterIDM = lastChapter;
            ViewBag.NameBook = book.BookName;
        }

        //[HttpGet]
        //public ActionResult Add()
        //{
        //    return View();
        //}


        [HttpPost]
        public ActionResult Add(ChapterDetailModelInput chapter)
        {
            if(ModelState.IsValid)
            {
                if(chapterRepo.IsContainInListChapterBook(chapter.IDBook,chapter.ChapterID))
                {
                    ModelState.AddModelError("", "ChapterID này đã tồn tại");
                }
                else
                {
                    ChapterDetail ch = new ChapterDetail
                    {
                        IDBook = chapter.IDBook,
                        ChapterID = chapter.ChapterID,
                        Alias = chapter.Alias,
                        NameChapter = chapter.NameChapter,
                        Content = chapter.Content
                    };
                    chapterRepo.Create(ch);
                    return RedirectToAction("Edit", "Book", new { id=chapter.IDBook});
                }

            }
            LoadData(chapter.IDBook);
            return View();
        }


        [HttpPost]
        public JsonResult Delete(int bookid,int chapterid)
        {
            try
            {
                var chapter = chapterRepo.getByID2(bookid,chapterid);
                chapterRepo.Delete(chapter);
                return Json(new { success = true, message = "Delete Successfully",idbook=bookid}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Search(string searchkey)
        {
            var data = new BookRepository().GetAllWithPageListSearch(1, 12, searchkey);
            return Json(new
            {
                data = data.Select(x => new
                {
                    BookID = x.BookID,
                    BookName = x.BookName,
                })
            }, JsonRequestBehavior.AllowGet);
        }



       [HttpGet]
       public ActionResult Edit(int idbook,int idChapter)
       {
            LoadData(idbook);
            var chapter = chapterRepo.getByID2(idbook, idChapter);
            var model = new ChapterDetailModelInput
            {
                IDBook = chapter.IDBook,
                ChapterID = chapter.ChapterID,
                Alias = chapter.Alias,
                NameChapter = chapter.NameChapter,
                Content = chapter.Content
            };
            return View("Edit", model);
       }


        [HttpPost]
        public ActionResult Edit(ChapterDetailModelInput chapter)
        {
            if (ModelState.IsValid)
            {
                    ChapterDetail ch = new ChapterDetail
                    {
                        IDBook = chapter.IDBook,
                        ChapterID = chapter.ChapterID,
                        Alias = chapter.Alias,
                        NameChapter = chapter.NameChapter,
                        Content = chapter.Content
                    };
                    chapterRepo.Update(ch);
                    return RedirectToAction("Edit", "Book", new { id = chapter.IDBook });

            }
            LoadData(chapter.IDBook);
            return View();
        }


    }
}