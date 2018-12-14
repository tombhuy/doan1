using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Areas.Admin.Models
{
    public class ChapterDetailModelInput
    {
        public int IDBook { get; set; }
        
        [Required]
        public int ChapterID { get; set; }

        [Required]
        public string Alias { get; set; }

        [Required]
        public string NameChapter { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }
}