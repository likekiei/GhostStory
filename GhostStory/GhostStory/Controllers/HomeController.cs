using GhostStory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace GhostStory.Controllers
{
    public class HomeController : Controller
    {
        private GhostStoryContext db = new GhostStoryContext();


        // GET: Home
        public ActionResult Index()
        {


            ViewBag.theme01 = db.Themes.Find("A").Category;
            ViewBag.theme02 = db.Themes.Find("B").Category;
            ViewBag.theme03 = db.Themes.Find("C").Category;
            ViewBag.theme04 = db.Themes.Find("D").Category;
            ViewBag.theme05 = db.Themes.Find("E").Category;
            ViewBag.theme06 = db.Themes.Find("F").Category;








            return View();
        }


        [ChildActionOnly]
        public ActionResult _IndexThemes03()
        {

            var post001 = db.Post.Where(x => x.themesID == "C").OrderByDescending(x => x.Publishtime).Take(5);

            return View(post001.ToList());

        }




        [ChildActionOnly]
        public ActionResult _IndexThemes06()
        {


            var post001 = db.Post.Where(x => x.themesID == "F").OrderByDescending(x => x.Publishtime).Take(5);

            return View(post001.ToList());

        }


        public ActionResult Details()
        {

            return View();


        }


    }

}