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
    [MemberLoginCheck]
    public class MemberDiscussesController : Controller
    {
        private GhostStoryContext db = new GhostStoryContext();

        // GET: MemberDiscusses
        public ActionResult Index()
        {
            //var s = Session["memberuser"].ToString();

            //ViewBag.account = s; 


            //ViewBag.useraccount = (string)Session["memberuser"];

            return View();
        }


    }

}
