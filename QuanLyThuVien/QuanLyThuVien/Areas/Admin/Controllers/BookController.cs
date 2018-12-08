using BusinessLogic.Repository;
using DataProvider.Model;
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
            var list = bookRepo.GetAllWithPageListSearch(page, 12, searchKey);
            //Lỗi A circular reference was detected while serializing an object of type 
            var result = JsonConvert.SerializeObject(ErrorResult.ErrorData, Formatting.Indented,
               new JsonSerializerSettings
               {
                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
               });
            return Json(new { data = result, pageNumber = result.PageCount, keyword = searchKey }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            IEnumerable<Author> listAuthor = authorRepo.GetAll();
            ViewBag.AuthorList = new SelectList(listAuthor, "AuthorID", "AuthorName");

            IEnumerable<BookCategory> listCategory = bookCategoryRepo.GetAll();
            ViewBag.ListCategory = new SelectList(listCategory, "CategoryID", "CategoryName");


            return View();
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            if(ModelState.IsValid)
            {
                bookRepo.Create(book);
                RedirectToAction("Index");
            }
            return View("Index");
        }

    }
}