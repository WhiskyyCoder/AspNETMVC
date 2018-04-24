using SimpleBlog.DAL;
using SimpleBlog.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class LoginController : Controller
    {
        private DAOContext _context = new DAOContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return Redirect("~/Home/Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Author author)
        {
            if (ModelState.IsValid)
            {

                var user = _context.getAuthors().Where(a => a.Username == author.Username && a.Pass == author.Pass).ToList();
                if (user.Count == 1)
                {
                    Session.Add("userid", user[0].IdAuthor);
                    Session.Add("user", user[0].Username);
                    return Redirect("~/Post/Manage");
                }
                TempData["Message"] = "Podane hasło lub login są złe!";
            }
            return View(author);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }

}