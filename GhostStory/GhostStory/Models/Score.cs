using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GhostStory.Models
{
    public class Score
    {
      

        [Key]
        [DisplayName("貼文編號")]
        [Column(Order = 1)]
        public string PostID { get; set; }

        [Key]
        [DisplayName("會員編號")]
        [Column(Order = 2)]
        public string MemberID { get; set; }


        [DisplayName("會員評分")]
        [StringLength(1)]
        public string Fraction { get; set; }


        //拉關聯
        public virtual Post Posts { get; set; }

        public virtual Member Members { get; set; }

    }
}