using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GhostStory.Models
{
    public class Themes
    {
        [Key]
        [DisplayName("主題編號")]
        [StringLength(1)]
        [RegularExpression("[A-Z]{1}")]
        [Required(ErrorMessage = "請填寫主題編號")]
        public string themesID { get; set; }

        [DisplayName("主題名稱")]
        [StringLength(20)]
        [Required(ErrorMessage = "請填寫主題名稱")]
        public string Category { get; set; }

       

    }
}