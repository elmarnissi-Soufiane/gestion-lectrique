using ExamenFinalVersio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ExamenFinalVersio.Controllers
{

    [Authorize]
    public class OuvrierController : Controller
    {

        public static db_ProjectEleEntities db = new db_ProjectEleEntities();
        // GET: Ouvrier
        public ActionResult Index()
        {
            IList<Ouvrier> ov = db.Ouvriers.ToList();
            return View(ov);
        }

        // GET: Ouvrier/Details/5
        public ActionResult Details(String id)
        {
            Ouvrier ov = db.Ouvriers.Find(id);
            return View(ov);
        }

        // GET: Ouvrier/Create
        public ActionResult Create()
        {
            Ouvrier model = new Ouvrier();

            var ovrFonction = new List<string> { "Chauffeur", "Peintre", "Chef d’équipe", "magasiniers" };
            model.FonctionOptions = new SelectList(ovrFonction);

            return View(model);
        }

        // POST: Ouvrier/Create
        [HttpPost]
        public ActionResult Create(Ouvrier ov)
        {
            try
            {

                string selectedFonction = Request.Form["fonction"];

                Ouvrier newOuvrier = new Ouvrier
                {
                    CIN = ov.CIN,
                    NomComplet = ov.NomComplet,
                    Ville = ov.Ville,
                    fonction = selectedFonction,
                    Telephone = ov.Telephone,
                    DateNaissance = ov.DateNaissance,
                    DateDebutActivite = ov.DateDebutActivite,
                };

                db.Ouvriers.Add(newOuvrier);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ouvrier/Edit/5
        public ActionResult Edit(String id)
        {
            Ouvrier ov = db.Ouvriers.Find(id);
            var ovrFonction = new List<string> { "Chauffeur", "Peintre", "Chef d’équipe", "magasiniers" };
            ViewBag.FonctionOptions = new SelectList(ovrFonction, ov.fonction);

            return View(ov);
        }

        // POST: Ouvrier/Edit/5
        [HttpPost]
        public ActionResult Edit(String id, Ouvrier Ouvriernew)
        {
            try
            {

                string selectedFonction = Request.Form["fonction"];

                Ouvrier oldOuvrier = db.Ouvriers.Find(id);

                oldOuvrier.NomComplet = Ouvriernew.NomComplet;
                oldOuvrier.Ville = Ouvriernew.Ville;
                oldOuvrier.fonction = selectedFonction;
                oldOuvrier.Telephone = Ouvriernew.Telephone;
                oldOuvrier.DateNaissance = Ouvriernew.DateNaissance;
                oldOuvrier.DateDebutActivite = Ouvriernew.DateDebutActivite;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ouvrier/Delete/5
        public ActionResult Delete(String id)
        {
            Ouvrier pl = db.Ouvriers.Find(id);
            return View(pl);
        }

        // POST: Ouvrier/Delete/5
        [HttpPost]
        public ActionResult Delete(String id, FormCollection collection)
        {
            try
            {
                Ouvrier ov = db.Ouvriers.Find(id);
                db.Ouvriers.Remove(ov);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
