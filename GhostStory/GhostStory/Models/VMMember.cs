using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace GhostStory.Models
{
    public class VMMember

    {

        [DisplayName("會員帳號")]
        [Required(ErrorMessage = "請填寫會員帳號")]
        [StringLength(20, ErrorMessage = "帳號不得超過20字")]

        public string Account { get; set; }


        [DisplayName("會員密碼")]
        [Required(ErrorMessage = "請填寫會員密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多20碼")]
        public string Password { get; set; }


        [DisplayName("再一次確認密碼")]
        [Required(ErrorMessage = "請確認會員密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多20碼")]
        [Compare("Password", ErrorMessage = "密碼輸入不一致，請重新確認輸入")]
        public string ConfirmPassword { get; set; }


        [DisplayName("姓名")]
        [StringLength(10, ErrorMessage = "姓名不得超過10個字")]
        [Required(ErrorMessage = "請填寫會員姓名")]
        public string Name { get; set; }

        [DisplayName("性別")]
        [Required(ErrorMessage = "請填寫性別")]
        public string Gender { get; set; }

        [DisplayName("電話")]
        [StringLength(20, ErrorMessage = "電話不得超過20個字")]
        [Required(ErrorMessage = "請填寫會員手機或電話")]
        public string Phone { get; set; }

        [DisplayName("地址")]
        [StringLength(100, ErrorMessage = "地址不得超過100個字")]
        [Required(ErrorMessage = "請填寫會員地址")]
        public string Address { get; set; }



        [DisplayName("電子信箱")]
        [StringLength(60, ErrorMessage = "E-mail不得超過60個字")]
        [Required(ErrorMessage = "請填寫電子信箱")]
        public string Email { get; set; }
    }

}






