using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GhostStory.Models;

namespace GhostStory.Controllers
{
    public class HomeManagerController : Controller
    {

        GhostStoryContext db = new GhostStoryContext();

        [LoginCheck]
        // GET: HomeManager
        public ActionResult Index(string id )
        {
            //加總會員人數
            var MemberCount = db.Member.Select(x => x.MemberID ).Count();
                ViewBag.memberCount = MemberCount;
            
            //各等級會員人數 A~E
            var MemberACount = db.Member.Where(x => x.Level == "A").Count();
            ViewBag.memberACount = MemberACount;

            var MemberBCount = db.Member.Where(x => x.Level == "B").Count();
            ViewBag.memberBCount = MemberBCount;


            var MemberCCount = db.Member.Where(x => x.Level == "C").Count();
            ViewBag.memberCCount = MemberCCount;

            var MemberDCount = db.Member.Where(x => x.Level == "D").Count();
            ViewBag.memberDCount = MemberDCount;

            var MemberECount = db.Member.Where(x => x.Level == "E").Count();
            ViewBag.memberECount = MemberECount;



            //加總文章數
            var PostCount = db.Post.Select(x => x.PostID).Count();
            ViewBag.postCount = PostCount;


            /////////各文章主題 
            ViewBag.theme01 = db.Themes.Find("A").Category;
            ViewBag.theme02 = db.Themes.Find("B").Category;
            ViewBag.theme03 = db.Themes.Find("C").Category;
            ViewBag.theme04 = db.Themes.Find("D").Category;
            ViewBag.theme05 = db.Themes.Find("E").Category;
            ViewBag.theme06 = db.Themes.Find("F").Category;

            //各文章數統計
            var PostACount = db.Post.Where(x => x.themesID == "A").Count();
            ViewBag.postACount = PostACount;

            var PostBCount = db.Post.Where(x => x.themesID == "B").Count();
            ViewBag.postBCount = PostBCount;

            var PostCCount = db.Post.Where(x => x.themesID == "C").Count();
            ViewBag.postCCount = PostCCount;

            var PostDCount = db.Post.Where(x => x.themesID == "D").Count();
            ViewBag.postDCount = PostDCount;

            var PostECount = db.Post.Where(x => x.themesID == "E").Count();
            ViewBag.postECount = PostECount;

            var PostFCount = db.Post.Where(x => x.themesID == "F").Count();
            ViewBag.postFCount = PostFCount;

            //if (Session["Administrators"]==null){
            //    return RedirectToAction("Index", "Home");
            //}




            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(VMHomeManagerLogin vMHomeManagerLogin)
        {
            var Administrators = db.Administrators.Where(n => n.Account == vMHomeManagerLogin.Account && n.Passwd == vMHomeManagerLogin.Passwd).FirstOrDefault() ;

            if (Administrators == null) {

                ViewBag.Errmsg01 = "帳號，密碼 錯誤請重新輸入!!";
                return View(vMHomeManagerLogin);
             

            }
            Session["Administrators"] = Administrators;

                return RedirectToAction("Index");

           
        }

        [LoginCheck]
        public ActionResult Logout()
        {

            Session["Administrators"] = null;
            return RedirectToAction("Index","Home");
        }


    }
}