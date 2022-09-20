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
    [LoginCheck]
    public class ThemesController : Controller
    {
        private GhostStoryContext db = new GhostStoryContext();

        // GET: Themes
        public ActionResult Index()
        {
            return View(db.Themes.ToList());
        }

        // GET: Themes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Themes themes = db.Themes.Find(id);
            if (themes == null)
            {
                return HttpNotFound();
            }
            return View(themes);
        }

        // GET: Themes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Themes/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "themesID,Category")] Themes themes)
        {
            if (ModelState.IsValid)
            {
                db.Themes.Add(themes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(themes);
        }

        // GET: Themes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Themes themes = db.Themes.Find(id);
            if (themes == null)
            {
                return HttpNotFound();
            }
            return View(themes);
        }

        // POST: Themes/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "themesID,Category")] Themes themes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(themes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(themes);
        }

        // GET: Themes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Themes themes = db.Themes.Find(id);
            if (themes == null)
            {
                return HttpNotFound();
            }
            return View(themes);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Themes themes = db.Themes.Find(id);
            db.Themes.Remove(themes);
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
