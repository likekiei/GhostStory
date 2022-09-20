using GhostStory.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace GhostStory.Controllers
{
    public class MemberController : Controller
    {
        private GhostStoryContext db = new GhostStoryContext();


        [MemberLoginCheck]
        // GET: Member
        public ActionResult Index()
        {
            return View();


        }


        public ActionResult Register()
        {
            return View();
        }


     

      
        // POST: Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "MemberID,Level,Account,Password,Name,Gender,Phone,Address,Email")] Member member)
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
                string PK = "A" + Convert.ToString(i + 1).PadLeft(9, '0');
                member.MemberID = PK;


                member.Level = "E";

                db.Member.Add(member);
                db.SaveChanges();

               

                return RedirectToAction("Login", "Member");
            }


            return View();


        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(VMMemberLogin vMMemberLogin)
        {
           

            var user = db.Member.Where(m => m.Account == vMMemberLogin.Account && m.Password == vMMemberLogin.Password).FirstOrDefault();
            
            //var member = db.Member.Where(m => m.Account == vMMemberLogin.Account && m.Password == vMMemberLogin.Password).FirstOrDefault();
            

            if (user==null) {
                ViewBag.ErrMsg = "帳號，密碼 錯誤請重新輸入!!!";
                return View(vMMemberLogin);
            }
            Session["user"] = user;
            Session["memberid"] = user.MemberID;
            Session["memberuser"] = user.Account;
            return RedirectToAction("Index", "AllPost");
        }


        [MemberLoginCheck]
        public ActionResult Logout()
        {

            Session["user"] = null;
            Session["memberid"] = null;
            return RedirectToAction("Index", "Home");
        }



        [MemberLoginCheck]
        public ActionResult Edit()
        {
            var id = Session["memberid"];
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

        [MemberLoginCheck]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member members)
        {

            string sql = "update Members set Name=@Name, Email=@Email,Phone=@Phone,Address=@Address where MemberID=@MemberID";

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GhostStoryConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Name", members.Name);
            cmd.Parameters.AddWithValue("@Email", members.Email);
            cmd.Parameters.AddWithValue("@Phone", members.Phone);
            cmd.Parameters.AddWithValue("@Address", members.Address);
            cmd.Parameters.AddWithValue("@MemberID", Session["memberid"]);

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index", "AllPost");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            conn.Close();

            return View(members);


        }



        [MemberLoginCheck]
        public ActionResult ChangePassword()
        {
            return View();

        }





        [MemberLoginCheck]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(VMChangePassword vMChangePassword)
        {
            if (ModelState.IsValid)
            {
                string sql = "update Members set Password=@Password where MemberID=@MemberID";

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GhostStoryConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, conn);

                var _user = db.Member.Where(m => m.Password == vMChangePassword.OriginalPassword).FirstOrDefault();

                if (_user != null)
                {

                    if (vMChangePassword.Password != null)
                    {


                        // 將新密碼一樣使用 SHA256 雜湊運算回存
                      
                      
                        byte[] hashValue;
                        string result01 = "";

                        System.Text.UnicodeEncoding ue = new System.Text.UnicodeEncoding();

                        byte[] pwBytes = ue.GetBytes(vMChangePassword.Password);

                        SHA256 shHash = SHA256.Create();

                        hashValue = shHash.ComputeHash(pwBytes);

                        foreach (byte b in hashValue)
                        {
                            result01 += b.ToString();
                        }

                        string NewPassword = result01.ToString();



                        cmd.Parameters.AddWithValue("@Password", NewPassword);
                        cmd.Parameters.AddWithValue("@MemberID", Session["memberid"]);
                        conn.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                            return RedirectToAction("Index", "AllPost");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = ex.Message;
                        }
                        conn.Close();

                        return View(vMChangePassword);





                    }



                    else
                    {
                        ViewBag.ErrMsg01 = "密碼 更正失敗!!!";
                        return View(vMChangePassword);
                    }



                }


                else
                {
                    ViewBag.ErrMsg = "原密碼錯誤請重新輸入!!!";
                    return View(vMChangePassword);
                }
            }
            return View(vMChangePassword);
        }










    }



}