using SimpleBlog.DAL;
using SimpleBlog.Views.Home;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class HomeController : Controller
    {
        private DAOContext _context = new DAOContext();
        // GET: Home
        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.Last5Posts = _context.getPosts().OrderByDescending(n => n.IdPost).Take(5).ToList();
            indexViewModel.NewPosts = _context.getPosts().OrderByDescending(n => n.IdPost).Take(10).ToList();

            var request = (from p in _context.getPosts()
                           join a in _context.getAuthors() on p.IdAuthor equals a.IdAuthor into zgrupowane
                           from nowe in zgrupowane.DefaultIfEmpty()
                           group nowe by nowe.Username into grouped
                           select new { id = grouped.Key, count = grouped.Count(n => n.Username == grouped.Key) });

            Dictionary<string, int> topAuthors = new Dictionary<string, int>();


            foreach (var z in request)
            {
                topAuthors.Add(z.id, z.count);
            }

            topAuthors = topAuthors.OrderByDescending(pair => pair.Value).Take(5)
            .ToDictionary(pair => pair.Key, pair => pair.Value);

            indexViewModel.TopAuthors = topAuthors;





            return View(indexViewModel);
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