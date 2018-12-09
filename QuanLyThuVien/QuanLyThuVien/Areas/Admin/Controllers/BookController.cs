using BusinessLogic.Repository;
using BusinessLogic.Repository.Base;
using DataProvider.Model;
using Newtonsoft.Json;
using QuanLyThuVien.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        // GET: Admin/Book
        BookRepository bookRepo=new BookRepository();
        AuthorRepository authorRepo = new AuthorRepository();
        BookCategoryRepository bookCategoryRepo = new BookCategoryRepository();
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult List(int page = 1, string searchKey = "")
        {
            var result = bookRepo.GetAllWithPageListSearch(page, 12, searchKey);
            //Lỗi A circular reference was detected while serializing an object of type 
            return Json(new { data = result.Select(x=>new {
                BookID=x.BookID,
                BookName=x.BookName,
                Description=x.Description,
                Alias=x.Alias,
                AuthorID=x.AuthorID,
                CategoryID=x.CategoryID,
                ViewCount=x.ViewCount,
                MoreImages =x.MoreImages,
                CreatedDate=x.CreatedDate,
                CreatedBy=x.CreatedBy,
                TopHot=x.TopHot
            }), pageNumber = result.PageCount, keyword = searchKey }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            IEnumerable<Author> listAuthor = authorRepo.GetAll();
            ViewBag.AuthorList = new SelectList(listAuthor, "AuthorID", "AuthorName");

            IEnumerable<BookCategory> listCategory = bookCategoryRepo.GetAll();
            ViewBag.ListCategory = new SelectList(listCategory, "CategoryID", "CategoryName");
            ViewBag.Title = "Thêm mới sách";

            return View();
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            if(ModelState.IsValid)
            {
                book.CreatedDate = DateTime.Now;
                var logininfo= (LoginInfor)Session[Constants.USERSESSION];
                book.CreatedBy = logininfo.UserID.ToString();
                bookRepo.Create(book);
                TempData["testmsg"] = " Add Successfully ";
                RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = bookRepo.GetById(id);
            IEnumerable<Author> listAuthor = authorRepo.GetAll();
            ViewBag.AuthorList = new SelectList(listAuthor, "AuthorID", "AuthorName");

            IEnumerable<BookCategory> listCategory = bookCategoryRepo.GetAll();
            ViewBag.ListCategory = new SelectList(listCategory, "CategoryID", "CategoryName");
            ViewBag.Title = "Chỉnh sửa";
            return View("Add", model);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                bookRepo.Update(book);
                TempData["testmsg"] = " Requested Successfully ";
                RedirectToAction("Index");
            }
            return View("Index");
        }


        [HttpPost]
        public JsonResult Delete(int ID)
        {
            try
            {
                var book = bookRepo.GetById(ID);
                bookRepo.Delete(book);
                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}