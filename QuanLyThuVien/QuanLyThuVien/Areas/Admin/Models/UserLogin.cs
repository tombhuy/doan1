using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyThuVien.Areas.Admin.Models
{
    [Serializable]
    public class UserLogin
    { 
        [Required(ErrorMessage ="Bạn chưa nhập username ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập password ")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}