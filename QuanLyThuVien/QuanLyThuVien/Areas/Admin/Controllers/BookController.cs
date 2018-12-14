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
        EbookRepository ebookRepo = new EbookRepository();
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
        public ActionResult Edit(int? id)
        {
            if(id!=null)
            {
                int idnew = (id ?? 1);
                var model = bookRepo.GetById(idnew);
                IEnumerable<Author> listAuthor = authorRepo.GetAll();
                ViewBag.AuthorList = new SelectList(listAuthor, "AuthorID", "AuthorName");

                IEnumerable<BookCategory> listCategory = bookCategoryRepo.GetAll();
                ViewBag.ListCategory = new SelectList(listCategory, "CategoryID", "CategoryName");
                ViewBag.Title = "Chỉnh sửa";
                return View("Edit", model);
            }
            else
            {
                return View("Index");
            }   
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

        public ActionResult GetAuthor()
        {
            var listAuthor = authorRepo.GetAll().Select(x=>new {
                AuthorID=x.AuthorID,
                AuthorName=x.AuthorName
            });
            return Json(listAuthor, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategory()
        {
            var listCategory = bookCategoryRepo.GetAll().Select(x => new {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName
            });
            return Json(listCategory, JsonRequestBehavior.AllowGet);
        }


        //Load Bảng Edit
        //Lấy danh sách chapter theo ID
        public ActionResult GetAllChapterByID(int page=1,int id=0)
        {
            var listChapter = new BookRepository().GetAllChapterByIDBook(page,5,id);
            return Json(new { data =listChapter.Select(x=>new {
                ID=x.ChapterID,
                NameChapter=x.NameChapter
            }), pageNumber = listChapter.PageCount,idbook=id,amountChapter=listChapter.Count}, JsonRequestBehavior.AllowGet);
        }

        //Lấy danh sách ebook theo ID
        public ActionResult GetAllEbookByIDBook(int page = 1, int idbook = 0)
        {
            var listEbook = new BookRepository().GetAllEbookByIDBook(page, 5, idbook);
            return Json(new
            {
                data = listEbook.Select(x => new {
                    BookID = idbook,
                    TypeEbookID =x.TypeEbookID,
                    TypeEbookName = x.TypeEbookName,
                    Link = x.Link
                }),
                pageNumber = listEbook.PageCount,
                idbook = idbook,
            }, JsonRequestBehavior.AllowGet);
        }

        //Lấy danh sách ebook hỗ trợ
        public ActionResult GetAllListTypeEbook()
        {

            var listTypeEbook = new TypeEbookRepository().GetAll().Select(x => new
            {
                TypeEbookName = x.Name,
                TypeEbookID = x.TypeID
            });
            return Json(listTypeEbook, JsonRequestBehavior.AllowGet);
        }
        
       //Thêm mới một ebook
        public ActionResult AddEbook(Ebook ebookObj)
        {
            try
            {
                ebookRepo.Create(ebookObj);
                return Json(new { success = true, message = "Add Successfully",idbook=ebookObj.BookID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        //Update một ebook
        public JsonResult GetEbookByID(int idbook,int typeid)
        {
            var ebook = ebookRepo.GetEbookByID2(idbook,typeid);
            return Json(new {
                BookID = idbook,
                TypeEbookID =ebook.TypeEbook,
                LinkDownload=ebook.Link
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult UpdateEbook(Ebook ebookObj)
        {
            try
            {
                ebookRepo.Update(ebookObj);
                return Json(new { success = true, message = "Update Successfully",idbook=ebookObj.BookID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteEbook(int idbook,int typeid)
        {
            try
            {
                var ebook = ebookRepo.GetEbookByID2(idbook,typeid);
                ebookRepo.Delete(ebook);
                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString(), idbook=idbook }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}