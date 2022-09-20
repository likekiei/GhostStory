using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using PagedList;

namespace GhostStory.Models
{
    public class Post
    {
        [Key]
        [DisplayName("貼文編號")]
        [StringLength(9)]
        [RegularExpression("[A-Z][0-9]{1,8}")]
      
        public string PostID { get; set; }

        
        [DisplayName("會員帳號")]
        
        public string Account { get; set; }
        

        [DisplayName("主題")]
        [Required]
        public string themesID { get; set; }


        [DisplayName("貼文時間")]
      
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy HH:mm:ss}", ApplyFormatInEditMode = true)]

        public DateTime Publishtime { get; set; }

        [DisplayName("貼文標體")]
        [Required(ErrorMessage = "請輸入貼文標題")]
        [StringLength(30, ErrorMessage = "貼文標題最多30個字")]
        public string Title { get; set; }


        [DisplayName("貼文內容")]
        [Required(ErrorMessage = "請輸入貼文內容")]
        [StringLength(3000, ErrorMessage = "貼文內容最多3000個字")]
        public string Content { get; set; }

        [DisplayName("故事地點")]
        [Required(ErrorMessage = "請輸入故事地點的大約地址")]
        [StringLength(100, ErrorMessage = "故事地點最多100個字")]
        public string location { get; set; }


        [DisplayName("有圖為證NO.1")]

        public string PostPhoto01 { get; set; }

        [DisplayName("有圖為證NO.2")]
        public string PostPhoto02 { get; set; }
       
        [DisplayName("有圖為證NO.3")]
        public string PostPhoto03 { get; set; }


        //拉關聯
        public virtual Themes Theme { get; set; }
        public virtual Member Members { get; set; }
       

    }
}