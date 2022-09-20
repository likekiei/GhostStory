using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GhostStory.Models;
using PagedList;

namespace GhostStory.Controllers
{
    [MemberLoginCheck]
    public class MemberPostsController : Controller
    {
        private GhostStoryContext db = new GhostStoryContext();



        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            
            //會員個人貼文數量
            var A01 = Session["memberuser"].ToString();
            var MemberPosrCount = db.Post.Where(x => x.Account == A01).Count();
            ViewBag.postCount = (10 - (MemberPosrCount % 10));
            
            ViewBag.MemberPosrCount = MemberPosrCount;

            //會員個人等級
            var MemberLevel = db.Member.Where(c => c.Account == A01).Select(c => c.Level).FirstOrDefault();
            ViewBag.memberLevel = MemberLevel;





            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;




            var post = from m in db.Post
                       select m;


            var z = Session["memberuser"].ToString();

            
                if (!String.IsNullOrEmpty(searchString))

                {
                   

                    post = post.Where(a => a.Title.Contains(searchString)).Where(x => x.Account == z).OrderByDescending(a => a.Publishtime);
                    if (post.Count() == 0)
                    {
                        ViewBag.err = "查無此筆文章!!";
                    }




                var post03 = post.Where(a => a.Title.Contains(searchString)).Where(x => x.Account == z).OrderByDescending(a => a.Publishtime);

                int pageSize01 = 10;
                int pageNumber01 = (page ?? 1);
                var post03List = post03.ToPagedList(pageNumber01, pageSize01);
                return View(post03List);


            }


          






            ////只抓會員本人文章,時間排序 //page分頁
            List<Post> p = new List<Post>();

            var s = Session["memberuser"].ToString();
            var p01 = db.Post.Where(x => x.Account == s).OrderByDescending(x => x.Publishtime);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(p01.ToPagedList(pageNumber, pageSize));



        }





        //會員等級貼文數量判斷
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberAccount"></param>
        /// <returns></returns>
        
        public string getLevel(string memberAccount)
        {

            int accountPostCount = db.Post.Where(x => x.Account == memberAccount).Count();
            if (accountPostCount < 10) { return "E"; }
            if (accountPostCount < 20) { return "D"; }
            if (accountPostCount < 30) { return "C"; }
            if (accountPostCount < 40) { return "B"; }
            if (accountPostCount < 50) { return "A"; }
            return "A";
        }





        // GET: MemberPosts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: MemberPosts/Create
        public ActionResult Create()
        {
            ViewBag.themesID = new SelectList(db.Themes, "themesID", "Category");
            return View();
        }

        // POST: MemberPosts/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,Account,themesID,Publishtime,Title,Content,location,PostPhoto01,PostPhoto02,PostPhoto03")] Post post,HttpPostedFileBase photo01 , HttpPostedFileBase photo02 , HttpPostedFileBase photo03)
        {
           


            

            if (photo01 != null)
            {
                   if (photo01.ContentLength > 0)
                    {
                        string extensionName = System.IO.Path.GetExtension(photo01.FileName);
                        if (extensionName == ".jpg" || extensionName == ".png")
                        {

                        string newFileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                                  

                            photo01.SaveAs(Server.MapPath("~/images/PostPhoto/"  +(string)Session["memberuser"]+ newFileName +"_01" + extensionName));

                            post.PostPhoto01 = (string)Session["memberuser"] + newFileName + "_01" + extensionName;
                        }
                    }
                

            }

            if (photo02 != null)
            {
                if (photo02.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo02.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png")
                    {

                        string newFileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));

                        photo02.SaveAs(Server.MapPath("~/images/PostPhoto/" + (string)Session["memberuser"]+ newFileName + "_02" + extensionName));

                        post.PostPhoto02 = (string)Session["memberuser"] + newFileName + "_02" + extensionName;
                    }
                }

            }

            if (photo03 != null)
            {
                if (photo03.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo03.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png")
                    {

                        string newFileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));

                        photo03.SaveAs(Server.MapPath("~/images/PostPhoto/" + (string)Session["memberuser"] + newFileName + "_03" + extensionName));

                        post.PostPhoto03 = (string)Session["memberuser"] + newFileName + "_03" + extensionName;
                    }

                }

            }


            if (ModelState.IsValid)
            {
              

                string PK = "";
                var result = db.Post.OrderByDescending(p => p.PostID).FirstOrDefault();
                if (result == null)
                {
                    PK = "P00000001";
                    post.PostID = PK;

                    db.Post.Add(post);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                string sa1 = result.PostID.Substring(2);

                int i = Convert.ToInt32(sa1);
                PK = "P" + Convert.ToString(i + 1).PadLeft(8, '0');
                post.PostID = PK;

                post.Account = (string)Session["memberuser"];

           



                db.Post.Add(post);
                db.SaveChanges();

                //會員等級貼文數量判斷
                string memberAccount = (string)Session["memberuser"];
                var postMember = db.Member.Where(x => x.Account == memberAccount).FirstOrDefault();
                postMember.Level = getLevel(memberAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.themesID = new SelectList(db.Themes, "themesID", "Category", post.themesID);
            return View(post);
        }

        // GET: MemberPosts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.themesID = new SelectList(db.Themes, "themesID", "Category", post.themesID);
            return View(post);
        }

        // POST: MemberPosts/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。


        [MemberLoginCheck]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Post post,  HttpPostedFileBase photo01, HttpPostedFileBase photo02, HttpPostedFileBase photo03, string id)
        {

            if (photo01 != null)
            {
                if (photo01.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo01.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png")
                    {

                        string newFileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));


                        photo01.SaveAs(Server.MapPath("~/images/PostPhoto/" + (string)Session["memberuser"] + newFileName + "_01" + extensionName));

                        post.PostPhoto01 = (string)Session["memberuser"] + newFileName + "_01" + extensionName;
                    }
                }


            }
            ModelState.Remove("PostPhoto01");

            if (photo02 != null)
            {
                if (photo02.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo02.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png")
                    {

                        string newFileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));

                        photo02.SaveAs(Server.MapPath("~/images/PostPhoto/" + (string)Session["memberuser"] + newFileName + "_02" + extensionName));

                        post.PostPhoto02 = (string)Session["memberuser"] + newFileName + "_02" + extensionName;
                    }
                }

            }
            ModelState.Remove("PostPhoto02");


            if (photo03 != null)
            {
                if (photo03.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo03.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png")
                    {

                        string newFileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));

                        photo03.SaveAs(Server.MapPath("~/images/PostPhoto/" + (string)Session["memberuser"] + newFileName + "_03" + extensionName));

                        post.PostPhoto03 = (string)Session["memberuser"] + newFileName + "_03" + extensionName;
                    }

                }

            }

            ModelState.Remove("PostPhoto03");



            if (ModelState.IsValid)
            {
                

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.themesID = new SelectList(db.Themes, "themesID", "Category", post.themesID);
            return View(post);


          


        }

        // GET: MemberPosts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: MemberPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
            //db.SaveChanges();


            db.SaveChanges();

            string memberAccount = (string)Session["memberuser"];
            var postMember = db.Member.Where(x => x.Account == memberAccount).FirstOrDefault();
            postMember.Level = getLevel(memberAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
