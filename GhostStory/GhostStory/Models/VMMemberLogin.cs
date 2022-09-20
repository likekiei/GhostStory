using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
namespace GhostStory.Models
{
    public class VMMemberLogin
    {

        [DisplayName("會員帳號")]
        [Required(ErrorMessage = "請填寫會員帳號")]
        [StringLength(20, ErrorMessage = "帳號不得超過20字")]

        public string Account { get; set; }

        string password;
        [DisplayName("會員密碼")]
        [Required(ErrorMessage = "請填寫會員密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多20碼")]
        
        public string Password {
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
    }
}