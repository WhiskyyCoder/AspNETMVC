using SimpleBlog.DAL;
using SimpleBlog.DAO;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SimpleBlog.Content
{
    public class PostController : Controller
    {
        private DAOContext _context = new DAOContext();
        // GET: Post
        public ActionResult Add()
        {
            if (Session["userid"] == null)
            {
                return Redirect("~/Login");
            }
                return View();
        }
        public ActionResult Success()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(Int32? pageid)
        {
            if (Session["userid"] != null)
            {
                if (pageid == null)
                {

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Nie podano id");

                }
                var page = _context.getContext().Post.Where(p => p.IdPost == pageid).ToList();

                if (page.Count == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Nie odnaleziono strony o podanym id");

                }
                Session["edit_id"] = pageid;
                return View(page[0]);
            }

            return Redirect("~/Login");
        }

        [HttpGet]
        public ActionResult Remove(Int32? pageid)
        {
            if (pageid == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Nie podano id");

            }
            var page = _context.getPosts().Where(p => p.IdPost == pageid).ToList();

            if (page.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Nie odnaleziono strony o podanym id");

            }

            @TempData["MessageSuccess"] = "Pomyslnie usunieto post: " + page[0].Title;
            _context.Delete(page[0]);
           
            return Redirect("~/Post/Success");
        }
        [HttpGet]   
        public ActionResult More(Int32? pageid)
        {
            if (pageid == null) {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"Nie podano id");

            }
            var page=_context.getPosts().Where(p=>p.IdPost==pageid).ToList();

            if (page.Count==0) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Nie odnaleziono strony o podanym id");

            }



            return View(page[0]);
        }


        public ActionResult Manage()
        {
            if (Session["userid"] != null)
            {
                int userId = (int)Session["userid"];
                var posts=_context.getContext().Post.Where(n=>n.Author.IdAuthor==userId).OrderByDescending(n => n.IdPost).Take(10).ToList();
                return View(posts);

            }
            return Redirect("~/Login");
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (Session["userid"] != null)
            {
                if (post == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Nie wypelniles formularza !");
                }
                else if (!ModelState.IsValid)
                {
                    return View(post);
                }
                int edit = ((int)Session["edit_id"]);
                var postBaza = _context.getContext().Post.Where(p=>p.IdPost==edit).SingleOrDefault();
                postBaza.ModifiedAt = DateTime.Now;
                postBaza.Title = post.Title;
                postBaza.Picture = post.Picture;
                postBaza.Content = post.Content;
                _context.getContext().SaveChanges();

                @TempData["MessageSuccess"] = "Pomyslnie zmodyfikowano post: "+ post.Title;
                return Redirect("~/Post/Success");

            }


            return Redirect("~/Login");






        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Post post)
        {
            if (Session["userid"] != null)
            {
                if (post == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Nie wypelniles formularza !");
                }
                else if (!ModelState.IsValid)
                {
                    return View(post);
                }
                int userId= (int)Session["userid"];
                post.Author = _context.getAuthors().Where(u=>u.IdAuthor==userId).SingleOrDefault();
                post.CreatedAt= DateTime.Now;
                post.ModifiedAt= DateTime.Now;
                _context.AddOrUpdate(post);

                @TempData["MessageSuccess"] = "Pomyslnie dodano post: " + post.Title;
                return Redirect("~/Post/Success");

            }


            return Redirect("~/Login");






        }
    }
}