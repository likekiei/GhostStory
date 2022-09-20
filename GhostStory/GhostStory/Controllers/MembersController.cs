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
    

    public class MembersController : Controller
    {
        private GhostStoryContext db = new GhostStoryContext();

        [LoginCheck]
        // GET: Members
        public ActionResult Index(string searchString, string currentFilter, int? page )
        {
          

          


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var member = from m in db.Member
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {

                member = member.Where(a => a.Account.Contains(searchString));
                if (member.Count() == 0)
                {
                    ViewBag.err = "查無此會員!!";
                }


                var member02 = member.Where(a => a.Account.Contains(searchString)).ToList();
                int pageNumber01 = (page ?? 1);
                int pagesize01 = 10;
                return View(member02.ToPagedList(pageNumber01, pagesize01));

            }


            List<Member> p = new List<Member>();
            var member03 = db.Member.ToList();
            int pageNumber = (page ?? 1);
            int pagesize = 10;
            return View(member03.ToPagedList(pageNumber, pagesize));


           





            //return View(db.Member.ToList());
        }
        
        [LoginCheck]
        // GET: Members/Details/5
        public ActionResult Details(string id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }


        [LoginCheck]
        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        [LoginCheck]
        // POST: Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,Level,Account,Password,Name,Gender,Phone,Address,Email")] Member member)
        {

            var account = db.Member.Where(m => m.Account == member.Account).FirstOrDefault();
            if (account != null)
            {
                ViewBag.Error = "此帳號有人使用";
                return View();

            }



            if (ModelState.IsValid)
            {



                string result = (from u in db.Member
                              orderby u.MemberID descending
                              select u.MemberID).FirstOrDefault();
                string sa1 = result.Substring(2);

                int i = Convert.ToInt32(sa1);
                string PK = "A" + Convert.ToString(i + 1).PadLeft(9,'0');
                member.MemberID = PK;


                member.Level = "E";

                db.Member.Add(member);
                db.SaveChanges();

                //if (Session[登入資訊] == 管理者) {
                //    return RedirectToAction(後臺首頁)
                //}
                //else {
                //Session[登入資訊]=登入狀態
                //    return RedirectToAction(前台首頁)
                //}

               

                return RedirectToAction("Index");
            }



            return View(member);





        }

        [LoginCheck]
        // GET: Members/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        [LoginCheck]
        // POST: Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,Level,Account,Password,Name,Gender,Phone,Address,Email")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        [LoginCheck]
        // GET: Members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        [LoginCheck]
        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Member member = db.Member.Find(id);
            db.Member.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [LoginCheck]
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
