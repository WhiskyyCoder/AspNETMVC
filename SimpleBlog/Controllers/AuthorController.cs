using SimpleBlog.DAL;
using SimpleBlog.Views.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class AuthorController : Controller
    {
        private DAOContext _context = new DAOContext();
        [HttpGet]
        public ActionResult Index(string name)
        {
            if (name == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Nie podano nazwy użytkownika");

            }
            var user = _context.getContext().Author.Where(a=>a.Username==name).ToList();

            if (user.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Nie odnaleziono strony o podanej nazwie użytkownika");

            }


            AuthorViewModel authorViewModel = new AuthorViewModel();
            authorViewModel.author = user[0];
            authorViewModel.countPosts = _context.getPosts().Count(p=>p.IdAuthor== authorViewModel.author.IdAuthor);
            authorViewModel.posts = _context.getPosts().OrderByDescending(p=>p.IdPost).Where(p => p.IdAuthor == authorViewModel.author.IdAuthor).Take(10).ToList();


            return View(authorViewModel);
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