using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GhostStory.Models
{
    public class VMHomeManagerLogin
    {

        [DisplayName("管理者帳號")]
        [Required(ErrorMessage = "請填寫帳號")]
        [StringLength(30, ErrorMessage = "帳號不得超過30字")]
        public string Account { get; set; }


        [DisplayName("管理者密碼")]
        [Required(ErrorMessage = "請填寫密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(30, ErrorMessage = "密碼最多30碼")]
        public string Passwd { get; set; }



    }
}