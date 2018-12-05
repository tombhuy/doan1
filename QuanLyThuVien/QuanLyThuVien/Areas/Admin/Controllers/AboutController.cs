using BusinessLogic.Repository;
using DataProvider.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    public class AboutController : Controller
    {
        // GET: Admin/About
        public ActionResult Index()
        {
            var aboutRpstory = new AboutRepository();
            var result = aboutRpstory.GetById(1);
            return View(result);
        }

        [HttpGet]
        public ActionResult ChinhSua(int id)
        {
            var aboutRpstory = new AboutRepository();
            var result = aboutRpstory.GetById(1);
            return View("Edit",result);
        }

        [HttpPost]
        public ActionResult ChinhSua(About model)
        {
            try
            {
                var aboutRpstory = new AboutRepository();
                aboutRpstory.Update(model);
            }
            catch (DbEntityValidationException e)
            {
                string errormessage = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    errormessage += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:/n", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        errormessage += String.Format("- Property: \"{0}\", Error: \"{1}\"/n", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return RedirectToAction("Index","About");
        }
    }
}