using BusinessLogic.Repository;
using DataProvider.Model;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    public class QuotationController : Controller
    {
        QuotationRepository quotationRepo=new QuotationRepository();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(int page = 1, string searchKey = "")
        {
            var result = quotationRepo.GetAllWithPageListSearch(page, 12, searchKey);
            return Json(new { data = result, pageNumber = result.PageCount, keyword = searchKey }, JsonRequestBehavior.AllowGet);
            //return Json(new { data = userRepo.GetAll() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(Quotation quo)
        {
            try
            {
                quotationRepo.Create(quo);
                return Json(new { success = true, message = "Add Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetbyID(int ID)
        {
            var quo = quotationRepo.GetById(ID);
            return Json(quo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Quotation quo)
        {
            try
            {
                quotationRepo.Update(quo);
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
                var quo = quotationRepo.GetById(ID);
                quotationRepo.Delete(quo);
                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}