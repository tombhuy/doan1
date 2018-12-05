using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyThuVien.Models
{
    public class QuotationModelView
    {
        
        public int QuotationID { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập nội dung")]
        [StringLength(1000)]
        public string NameQuotation { get; set; }
    }
}