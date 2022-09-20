using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GhostStory.Models;

namespace GhostStory.Controllers
{
  

    public class ScoresController : Controller
    {
        private GhostStoryContext db = new GhostStoryContext();

        [MemberLoginCheck]
        public ActionResult Score(string postID, string memberId,string Fraction)
        {
            Score score = new Score();
            var scoredetail = db.Score.Where(p => p.MemberID == memberId && p.PostID == postID).FirstOrDefault();

            if (scoredetail == null)
            { 

                score.PostID = postID;
                score.MemberID = memberId;
                score.Fraction = Fraction;
                db.Score.Add(score);
                db.SaveChanges();
                //return Redirect("/AllPost/Details/" + postID);
                return RedirectToAction("Details", "AllPost", new { id = postID });
            }
            else
            {
                if (scoredetail.Fraction == Fraction)
                {
                    db.Score.Remove(scoredetail);
                    db.SaveChanges();
                }
                else {
                    scoredetail.Fraction = Fraction;
                    db.SaveChanges();
                }
                return Redirect("/AllPost/Details/" + postID);
                //return RedirectToAction("Details", "AllPost", new {id = postID });

            }

        }



        [LoginCheck]
        // GET: Scores
        public ActionResult Index()
        {
            var score = db.Score.Include(s => s.Members).Include(s => s.Posts);
            return View(score.ToList());
        }
        [LoginCheck]
        // GET: Scores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Score.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }
        [LoginCheck]
        // GET: Scores/Create
        public ActionResult Create()
        {
            ViewBag.MemberID = new SelectList(db.Member, "MemberID", "Level");
            ViewBag.PostID = new SelectList(db.Post, "PostID", "Account");
            return View();
        }
        [LoginCheck]
        // POST: Scores/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,MemberID,Fraction")] Score score)
        {
            if (ModelState.IsValid)
            {
                db.Score.Add(score);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberID = new SelectList(db.Member, "MemberID", "Level", score.MemberID);
            ViewBag.PostID = new SelectList(db.Post, "PostID", "Account", score.PostID);
            return View(score);
        }
      
        [LoginCheck]
        // GET: Scores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Score.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Member, "MemberID", "Level", score.MemberID);
            ViewBag.PostID = new SelectList(db.Post, "PostID", "Account", score.PostID);
            return View(score);
        }

        [LoginCheck]
        // POST: Scores/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,MemberID,Fraction")] Score score)
        {
            if (ModelState.IsValid)
            {
                db.Entry(score).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.Member, "MemberID", "Level", score.MemberID);
            ViewBag.PostID = new SelectList(db.Post, "PostID", "Account", score.PostID);
            return View(score);
        }

        [LoginCheck]
        // GET: Scores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Score.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }


        [LoginCheck]
        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Score score = db.Score.Find(id);
            db.Score.Remove(score);
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
