using BusinessLogic.Repository;
using DataProvider.Model;
using DataProvider.ModelView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    public class AudioController : Controller
    {
        // GET: Admin/AudioBook
        AudioBookRepository audioRepo = new AudioBookRepository();
        public ActionResult Index()
        {
            var listBook = new BookRepository().GetAll().Select(x => new
            {
                idBook = x.BookID,
                nameBook = x.BookName
            });
            ViewBag.ListBook = new SelectList(listBook, "idBook", "nameBook");

            var audioList = new AudioBookRepository().ListAllAudioBook();
            return View("Index",audioList);
        }

        [HttpGet]
        public ActionResult UploadAudio()
        {
            var audioList = new AudioBookRepository().ListAllAudioBook();
            return View(audioList);
        }

        [HttpPost]
        public ActionResult UploadAudio(HttpPostedFileBase fileupload)
        {
            if (fileupload != null)
            {
                string fileName = Path.GetFileName(fileupload.FileName);
                int fileSize = fileupload.ContentLength;
                int Size = fileSize / 1000000;
                fileupload.SaveAs(Server.MapPath("~/AudioFileUpload/" + fileName));

                //AudioBook audio = new AudioBook();
                //new AudioBookRepository().Create(fileupload);
            }
            return RedirectToAction("UploadAudio");
        }


        [HttpPost]
        public ActionResult UploadAudio2(AudioBookModelView model)
        {
            string fileName = Path.GetFileName(model.File.FileName);
            int fileSize = model.File.ContentLength;
            int Size = fileSize / 1000000;
            model.File.SaveAs(Server.MapPath("~/AudioFileUpload/" + fileName));
            AudioBook audio = new AudioBook
            {
                BookID=model.BookID,
                AudioID=model.AudioID,
                AudioName=model.AudioName,
                FileAudioSize=Size,
                FileAudioPath= "~/AudioFileUpload/" + fileName,
                Alias=model.Alias
            };
            audioRepo.Create(audio);
            return RedirectToAction("UploadAudio");
        }

        public ActionResult GetListBook()
        {
            var listBook = new BookRepository().GetAll().Select(x => new
            {
                idBook=x.BookID,
                nameBook=x.BookName
            });

            return Json(listBook, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Audio()
        {
            return View();
        }
    }
}