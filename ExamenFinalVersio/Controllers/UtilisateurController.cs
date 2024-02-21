using ExamenFinalVersio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace ExamenFinalVersio.Controllers
{
    public class UtilisateurController : Controller
    {
        db_ProjectEleEntities d = new db_ProjectEleEntities();

        public static int cpt = 0;

        public ActionResult Login()
        {
            ViewBag.Layout = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel util)
        {
            ViewBag.Layout = null;
            bool userFind = d.Utilisateurs.Any(u => util.username == u.username && u.PasswordL == util.PasswordL);

            var user = d.Utilisateurs.Where(u => util.username == u.username && u.PasswordL == util.PasswordL).FirstOrDefault();

            if (userFind)
            {
                FormsAuthentication.SetAuthCookie(user.username, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Utilisateur u)
        {
            d.Utilisateurs.Add(u);
            d.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult LogoutAction()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}