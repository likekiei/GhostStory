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
    [LoginCheck]
    public class PostsController : Controller
    {
        private GhostStoryContext db = new GhostStoryContext();

        // GET: Posts
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {

            //var post = db.Post.Include(p => p.Theme);
            //return View(post.ToList());




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
                if (post01.Count() == 0)
                {
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

        // GET: Posts/Details/5
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

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.themesID = new SelectList(db.Themes, "themesID", "Category");
            return View();
        }

        // POST: Posts/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Post post, HttpPostedFileBase photo01, HttpPostedFileBase photo02, HttpPostedFileBase photo03)
        {


            if (photo01 != null)
            {
                if (photo01.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo01.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png")
                    {

                        string newFileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));


                        photo01.SaveAs(Server.MapPath("~/images/PostPhoto/" + "Administrators" + newFileName + "_01" + extensionName));

                        post.PostPhoto01 = "Administrators" + newFileName + "_01" + extensionName;
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

                        photo02.SaveAs(Server.MapPath("~/images/PostPhoto/" + "Administrators" + newFileName + "_02" + extensionName));

                        post.PostPhoto02 = "Administrators" + newFileName + "_02" + extensionName;
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

                        photo03.SaveAs(Server.MapPath("~/images/PostPhoto/" + "Administrators" + newFileName + "_03" + extensionName));

                        post.PostPhoto03 = "Administrators" + newFileName + "_03" + extensionName;
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

                post.Account = "管理者";

                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.themesID = new SelectList(db.Themes, "themesID", "Category", post.themesID);
            return View(post);
        }

        // GET: Posts/Edit/5
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

        // POST: Posts/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post, HttpPostedFileBase photo01, HttpPostedFileBase photo02, HttpPostedFileBase photo03)
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

        // GET: Posts/Delete/5
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

        // POST: Posts/Delete/5
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
