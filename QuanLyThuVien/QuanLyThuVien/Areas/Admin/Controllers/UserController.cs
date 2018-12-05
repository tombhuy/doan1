using BusinessLogic.Repository.Base;
using DataProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        UserRepository userRepo= new UserRepository();
        public ActionResult Index()
        { 
            return View();
        }

        //public JsonResult List(int page=1)
        //{
        //    var result = userRepo.GetAllWithPageList(page, 12);
        //    return Json(new { data = result,pageNumber=result.PageCount}, JsonRequestBehavior.AllowGet);
        //    //return Json(new { data = userRepo.GetAll() }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult List(int page = 1,string searchKey="")
        {
            var result = userRepo.GetAllWithPageListSearch(page, 12,searchKey);
            return Json(new { data = result, pageNumber = result.PageCount,keyword=searchKey}, JsonRequestBehavior.AllowGet);
            //return Json(new { data = userRepo.GetAll() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(User usr)
        {
            try
            {
                usr.CreatedDate = DateTime.Now;
                userRepo.Create(usr);
                return Json(new {success=true,message="Add Successfully"}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new {success=false,message=ex.ToString()}, JsonRequestBehavior.AllowGet);
            }
            
        }
        public JsonResult GetbyID(int ID)
        {
            var user = userRepo.GetById(ID);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(User usr)
        {
            try
            {
                usr.CreatedDate = DateTime.Now;
                userRepo.Update(usr);
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
                var usr = userRepo.GetById(ID);
                userRepo.Delete(usr);
                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}