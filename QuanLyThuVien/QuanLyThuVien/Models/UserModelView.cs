using DataProvider.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyThuVien.Models
{
    public class UserModelView
    {

        public long UserID { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập username")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập password")]
        [StringLength(50)]
        public string Password { get; set; }

        public bool RemmemberMe { get; set; }
    }
}