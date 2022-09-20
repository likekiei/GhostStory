using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace GhostStory.Models
{
    public class Member
    {
        [Key]
        [DisplayName("會員編號")]
        public string MemberID { get; set; }

        [DisplayName("會員等級")]
        [StringLength(1)]
        public string Level { get; set; }

        
        [DisplayName("會員帳號")]
        [Required]
        [StringLength(20)]
        //[CheckAccount]
        public string Account { get; set; }

       

        string password;  //定義一個password的field

        [DisplayName("會員密碼")]
        [Required]
        [DataType(DataType.Password)]

        public string Password
        {
            get
            {
                return password;

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

                password = result;
            }

        }





        [DisplayName("姓名")]
        [StringLength(10)]
        [Required]
        public string Name { get; set; }

        [DisplayName("性別")]
        [Required]
        public string Gender { get; set; }

        [DisplayName("電話")]
        [StringLength(20)]
        [Required]
        public string Phone { get; set; }

        [DisplayName("地址")]
        [StringLength(100)]
        [Required]
        public string Address { get; set; }

        [DisplayName("電子信箱")]
        [StringLength(60)]
        [Required]
        public string Email { get; set; }



        //驗證重複帳號
        //public class CheckAccount : ValidationAttribute
        //{

        //    public CheckAccount()
        //    {

        //        ErrorMessage = "帳號已有人使用";
        //    }


        //    public override bool IsValid(object value)
        //    {
        //        GhostStoryContext db = new GhostStoryContext();
        //        var account = db.Member.Where(m => m.Account == value.ToString()).FirstOrDefault();
        //        return (account == null) ? true : false;
        //    }

        //}






    }



}


