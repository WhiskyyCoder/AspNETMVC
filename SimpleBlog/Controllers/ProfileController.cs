using SimpleBlog.DAL;
using SimpleBlog.Views.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class ProfileController : Controller
    {
        private DAOContext _context = new DAOContext();
        public ActionResult Password()
        {
            if (Session["userid"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("~/Login");
            }
        }
        [HttpPost]
        public ActionResult Password(PasswordViewModel model)
        {
            if (Session["userid"] != null)
            {
                if (ModelState.IsValid)
                {
                    if (model.newPaswword == model.reNewPassword)
                    {
                        int idAuthor = (int)Session["userid"];
                        var author = _context.getAuthors().Where(n => n.Pass == model.oldPassword && n.IdAuthor == idAuthor).SingleOrDefault();
                        if (author != null)
                        {

                            author.Pass = model.newPaswword;
                            _context.getContext().SaveChanges();
                            TempData["MessageSuccess"] = "Pomyślnie zmieniono hasło !";
                            return View(model);
                        }
                        TempData["Message"] = "Stare hasło nie pasuje";


                    }
                    else
                    {
                        TempData["Message"] = "Podane nowe hasła nie pasują do siebie";
                    }
                }



                return View(model);
            }
            else
            {
                return Redirect("~/Login");
            }



        }
    }
}