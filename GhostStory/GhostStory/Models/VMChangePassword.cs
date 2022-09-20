using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Security.Cryptography;

namespace GhostStory.Models
{
    public class VMChangePassword
    {



        /// 原密碼
        string originalPassword;
        [DisplayName("請輸入原密碼")]
        [Required(ErrorMessage = "請填寫會員密碼")]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多20碼")]
        [DataType(DataType.Password)]
        public string OriginalPassword
        {

            get
            {
                return originalPassword;

            }
            set
            {
                byte[] hashValue;
                string result = "";

                System.Text.UnicodeEncoding ue = new System.Text.UnicodeEncoding();

                byte[] pwBytes = ue.GetBytes(value);

                SHA256 shHash = SHA256.Create();

                hashValue = shHash.ComputeHash(pwBytes);

                foreach (byte b in hashValue)
                {
                    result += b.ToString();
                }

                originalPassword = result;
            }





        }



        /// 新密碼
        [DisplayName("請輸入新密碼")]
        [Required(ErrorMessage = "請填寫會員密碼")]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多20碼")]
        [DataType(DataType.Password)]
        public string Password  { get; set; }









            [DisplayName("再一次確認新密碼")]
        [Required(ErrorMessage = "請確認會員密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多20碼")]
        [Compare("Password", ErrorMessage = "新密碼輸入不一致，請重新確認輸入")]
        public string ConfirmPassword { get; set; }


    }
}