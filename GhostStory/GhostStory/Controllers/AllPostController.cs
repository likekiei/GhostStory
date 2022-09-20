using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GhostStory.Models;
using PagedList;

namespace GhostStory.Controllers
{
    
    public class AllPostController : Controller
    {
        private GhostStoryContext db = new GhostStoryContext();


      
       

        [MemberLoginCheck]
        // GET: AllPost



        public ActionResult Index(string searchString,string currentFilter, int? page)
        {
           

            ViewBag.theme01 = db.Themes.Find("A").Category;
            ViewBag.theme02 = db.Themes.Find("B").Category;
            ViewBag.theme03 = db.Themes.Find("C").Category;
            ViewBag.theme04 = db.Themes.Find("D").Category;
            ViewBag.theme05 = db.Themes.Find("E").Category;
            ViewBag.theme06 = db.Themes.Find("F").Category;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


                var post01 = from m in db.Post
                             select m;

                if (!String.IsNullOrEmpty(searchString))
                {

                    post01 = post01.Where(a => a.Title.Contains(searchString)).OrderByDescending(a => a.Publishtime);
                    if (post01.Count() == 0) {
                        ViewBag.err = "查無此筆文章!!";
                    }


                var post02 = post01.Where(a => a.Title.Contains(searchString)).OrderByDescending(a => a.Publishtime);
                int pageNumber01 = (page ?? 1);
                 int pagesize01 = 10;
                return View(post02.ToPagedList(pageNumber01, pagesize01));




                }


            ////抓主題文章,時間排序 //page分頁
            List<Post> p = new List<Post>();
            var post = db.Post.Include(a => a.Theme).OrderByDescending(a => a.Publishtime);
            int pageNumber = (page ?? 1);
            int pagesize = 10;
            return View(post.ToPagedList(pageNumber, pagesize));


           
        }





        [MemberLoginCheck]
        public ActionResult Themes01(string searchString, string currentFilter, int? page)
        {

            ViewBag.theme01 = db.Themes.Find("A").Category;
            ViewBag.theme02 = db.Themes.Find("B").Category;
            ViewBag.theme03 = db.Themes.Find("C").Category;
            ViewBag.theme04 = db.Themes.Find("D").Category;
            ViewBag.theme05 = db.Themes.Find("E").Category;
            ViewBag.theme06 = db.Themes.Find("F").Category;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var post01 = from m in db.Post
                             select m;

                if (!String.IsNullOrEmpty(searchString))
                {

                    post01 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "A").OrderByDescending(a => a.Publishtime);
                    if (post01.Count() == 0)
                    {
                      

                        ViewBag.err = "查無此筆文章!!";
                    }

                    //page分頁
                    
              

                 

                    var post02 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "A").OrderByDescending(a => a.Publishtime);

                   int pageNumber01 = (page ?? 1);
                   int pagesize01 = 10;
                   return View(post02.ToPagedList(pageNumber01, pagesize01));
               


                }

             

            


            //page分頁
         

           //只抓主題文章,時間排序
            //List<Post> p = new List<Post>();



            var post001 = db.Post.Where(x => x.themesID == "A").OrderByDescending(x => x.Publishtime).ToList();
            int pagesize = 10;
            int pageNumber02 = (page ?? 1);
            var post01List = post001.ToPagedList(pageNumber02, pagesize);
         
            return View(post01List);

        }



        [MemberLoginCheck]
        public ActionResult Themes02(string searchString, string currentFilter, int? page)
        {


            ViewBag.theme01 = db.Themes.Find("A").Category;
            ViewBag.theme02 = db.Themes.Find("B").Category;
            ViewBag.theme03 = db.Themes.Find("C").Category;
            ViewBag.theme04 = db.Themes.Find("D").Category;
            ViewBag.theme05 = db.Themes.Find("E").Category;
            ViewBag.theme06 = db.Themes.Find("F").Category;


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var post01 = from m in db.Post
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {

                post01 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "B").OrderByDescending(a => a.Publishtime);
                if (post01.Count() == 0)
                {


                    ViewBag.err = "查無此筆文章!!";
                }

                //page分頁





                var post02 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "B").OrderByDescending(a => a.Publishtime);

                int pageNumber01 = (page ?? 1);
                int pagesize01 = 10;
                return View(post02.ToPagedList(pageNumber01, pagesize01));

            }

            //只抓主題文章,時間排序
            //List<Post> p = new List<Post>();



            var post001 = db.Post.Where(x => x.themesID == "B").OrderByDescending(x => x.Publishtime).ToList();
            int pagesize = 10;
            int pageNumber02 = (page ?? 1);
            var post01List = post001.ToPagedList(pageNumber02, pagesize);

            return View(post01List);


        }




        public ActionResult Themes03(string searchString, string currentFilter, int? page)
        {


            ViewBag.theme01 = db.Themes.Find("A").Category;
            ViewBag.theme02 = db.Themes.Find("B").Category;
            ViewBag.theme03 = db.Themes.Find("C").Category;
            ViewBag.theme04 = db.Themes.Find("D").Category;
            ViewBag.theme05 = db.Themes.Find("E").Category;
            ViewBag.theme06 = db.Themes.Find("F").Category;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var post01 = from m in db.Post
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {

                post01 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "C").OrderByDescending(a => a.Publishtime);
                if (post01.Count() == 0)
                {


                    ViewBag.err = "查無此筆文章!!";
                }

                //page分頁





                var post02 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "C").OrderByDescending(a => a.Publishtime);

                int pageNumber01 = (page ?? 1);
                int pagesize01 = 10;
                return View(post02.ToPagedList(pageNumber01, pagesize01));

            }

            //只抓主題文章,時間排序
            //List<Post> p = new List<Post>();



            var post001 = db.Post.Where(x => x.themesID == "C").OrderByDescending(x => x.Publishtime).ToList();
            int pagesize = 10;
            int pageNumber02 = (page ?? 1);
            var post01List = post001.ToPagedList(pageNumber02, pagesize);

            return View(post01List);

        }


        [MemberLoginCheck]
        public ActionResult Themes04(string searchString, string currentFilter, int? page)
        {


            ViewBag.theme01 = db.Themes.Find("A").Category;
            ViewBag.theme02 = db.Themes.Find("B").Category;
            ViewBag.theme03 = db.Themes.Find("C").Category;
            ViewBag.theme04 = db.Themes.Find("D").Category;
            ViewBag.theme05 = db.Themes.Find("E").Category;
            ViewBag.theme06 = db.Themes.Find("F").Category;



            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var post01 = from m in db.Post
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {

                post01 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "D").OrderByDescending(a => a.Publishtime);
                if (post01.Count() == 0)
                {


                    ViewBag.err = "查無此筆文章!!";
                }

                //page分頁





                var post02 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "D").OrderByDescending(a => a.Publishtime);

                int pageNumber01 = (page ?? 1);
                int pagesize01 = 10;
                return View(post02.ToPagedList(pageNumber01, pagesize01));

            }

            //只抓主題文章,時間排序
            //List<Post> p = new List<Post>();



            var post001 = db.Post.Where(x => x.themesID == "D").OrderByDescending(x => x.Publishtime).ToList();
            int pagesize = 10;
            int pageNumber02 = (page ?? 1);
            var post01List = post001.ToPagedList(pageNumber02, pagesize);

            return View(post01List);

        }


        [MemberLoginCheck]
        public ActionResult Themes05(string searchString, string currentFilter, int? page)
        {


            ViewBag.theme01 = db.Themes.Find("A").Category;
            ViewBag.theme02 = db.Themes.Find("B").Category;
            ViewBag.theme03 = db.Themes.Find("C").Category;
            ViewBag.theme04 = db.Themes.Find("D").Category;
            ViewBag.theme05 = db.Themes.Find("E").Category;
            ViewBag.theme06 = db.Themes.Find("F").Category;


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var post01 = from m in db.Post
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {

                post01 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "E").OrderByDescending(a => a.Publishtime);
                if (post01.Count() == 0)
                {


                    ViewBag.err = "查無此筆文章!!";
                }

                //page分頁





                var post02 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "E").OrderByDescending(a => a.Publishtime);

                int pageNumber01 = (page ?? 1);
                int pagesize01 = 10;
                return View(post02.ToPagedList(pageNumber01, pagesize01));

            }

            //只抓主題文章,時間排序
            //List<Post> p = new List<Post>();



            var post001 = db.Post.Where(x => x.themesID == "E").OrderByDescending(x => x.Publishtime).ToList();
            int pagesize = 10;
            int pageNumber02 = (page ?? 1);
            var post01List = post001.ToPagedList(pageNumber02, pagesize);

            return View(post01List);
        }








        public ActionResult Themes06(string searchString, string currentFilter, int? page)
        {


            ViewBag.theme01 = db.Themes.Find("A").Category;
            ViewBag.theme02 = db.Themes.Find("B").Category;
            ViewBag.theme03 = db.Themes.Find("C").Category;
            ViewBag.theme04 = db.Themes.Find("D").Category;
            ViewBag.theme05 = db.Themes.Find("E").Category;
            ViewBag.theme06 = db.Themes.Find("F").Category;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var post01 = from m in db.Post
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {

                post01 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "F").OrderByDescending(a => a.Publishtime);
                if (post01.Count() == 0)
                {


                    ViewBag.err = "查無此筆文章!!";
                }

                //page分頁





                var post02 = post01.Where(a => a.Title.Contains(searchString)).Where(x => x.themesID == "F").OrderByDescending(a => a.Publishtime);

                int pageNumber01 = (page ?? 1);
                int pagesize01 = 10;
                return View(post02.ToPagedList(pageNumber01, pagesize01));

            }

            //只抓主題文章,時間排序
            //List<Post> p = new List<Post>();



            var post001 = db.Post.Where(x => x.themesID == "F").OrderByDescending(x => x.Publishtime).ToList();
            int pagesize = 10;
            int pageNumber02 = (page ?? 1);
            var post01List = post001.ToPagedList(pageNumber02, pagesize);

            return View(post01List);

        }





        //[MemberLoginCheck]
        // GET: AllPost/Details/5
        public ActionResult Details(string id, string fraction)
        {

            string score = "";
            if (Session["memberid"] != null)
            {
                var memberid = (string)Session["memberid"];
                
                var likeYN = db.Score.Where(x => x.PostID == id && x.MemberID == memberid).FirstOrDefault();


                if (likeYN != null)
                {
                    var fraction01 = likeYN.Fraction.ToString();
                    score = "您已評分~";
                    ViewBag.Fraction = fraction01;
                }
                else {
                    score = "尚未評分!!";
                } 

                ViewBag.Score = score;
                
                            

            }


            //分數除會員id = 平均分數!

        



            //var scoreID= db.Score.Where(w => w.PostID == id).Count();
            

            //var scoreFraction = db.Score.Select (a =>  a.Fraction ).Sum(int.Parse);

            //string xx = "";

          

            //switch (scoreFraction / scoreID)
            //{

            //    case 1:
            //        xx = "完全不恐怖!";
            //        break;

            //    case 2:
            //        xx = "有點相信!!";
            //        break;
            //    case 3:
            //        xx = "有點恐怖~!!";
            //        break;
            //    case 4:
            //        xx = "恐怖!後悔看了這內容!!";
            //        break;
            //    case 5:
            //        xx = "完了!今天不用睡了…!";
            //        break;
            //};


            //ViewBag.Score001 = xx;



            //加總這篇文章評分(1)的人數
            var score01 = db.Score.Where(x => x.PostID == id ).Where( q => q.Fraction == "1").Count();
            ViewBag.Score01 = score01;


            //加總這篇文章評分(2)的人數
            var score02 = db.Score.Where(x => x.PostID == id).Where(q => q.Fraction == "2").Count();
            ViewBag.Score02 = score02;

            //加總這篇文章評分(3)的人數
            var score03 = db.Score.Where(x => x.PostID == id).Where(q => q.Fraction == "3").Count();
            ViewBag.Score03 = score03;

            //加總這篇文章評分(4)的人數
            var score04 = db.Score.Where(x => x.PostID == id).Where(q => q.Fraction == "4").Count();
            ViewBag.Score04 = score04;

            //加總這篇文章評分(5)的人數
            var score05 = db.Score.Where(x => x.PostID == id).Where(q => q.Fraction == "5").Count();
            ViewBag.Score05 = score05;



            //加總這篇文章所有評分的人數
            var likeCount = db.Score.Where(x => x.PostID == id).Count();
           
          
            ViewBag.likeCount = likeCount;



            var content = db.Post.Find(id);
            if (content == null)
            {
                return RedirectToAction("Index");
            }
            //var content = db.Article.Where(x=>x.ArticleId == id).ToList().FirstOrDefault();
            ViewBag.content = content.ToString();
            ViewBag.user = Session["memberid"];
            return View(content);








            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Post post = db.Post.Find(id);
            //if (post == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(post);
        }





        [MemberLoginCheck]
        // GET: AllPost/Create
        public ActionResult Create()
        {
            ViewBag.themesID = new SelectList(db.Themes, "themesID", "Category");
            return View();
        }

        // POST: AllPost/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,Account,themesID,Publishtime,Title,Content,location,PostPhoto01,PostPhoto02,PostPhoto03")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.themesID = new SelectList(db.Themes, "themesID", "Category", post.themesID);
            return View(post);
        }
        [MemberLoginCheck]
        // GET: AllPost/Edit/5
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
        [MemberLoginCheck]
        // POST: AllPost/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,Account,themesID,Publishtime,Title,Content,location,PostPhoto01,PostPhoto02,PostPhoto03")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.themesID = new SelectList(db.Themes, "themesID", "Category", post.themesID);
            return View(post);
        }
        [MemberLoginCheck]
        // GET: AllPost/Delete/5
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
        [MemberLoginCheck]
        // POST: AllPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
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
