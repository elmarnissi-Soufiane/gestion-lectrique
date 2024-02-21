using ExamenFinalVersio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenFinalVersio.Controllers
{

    [Authorize]
    public class PaiementController : Controller
    {

        public static db_ProjectEleEntities db = new db_ProjectEleEntities();

        // GET: Paiement
        public ActionResult Index()
        {
            IList<Paiement> pm = db.Paiements.ToList();
            return View(pm);
        }

        // GET: Paiement/Details/5
        public ActionResult Details(int id)
        {
            Paiement pm = db.Paiements.Find(id);
            return View(pm);
        }

        // GET: Paiement/Create
        public ActionResult Create()
        {
            Paiement model = new Paiement();

            // Populate the dropdown list with the Immatricule values
            var icin = db.Ouvriers.Select(v => v.CIN).ToList();
            model.ICIN = new SelectList(icin);

            return View(model);
        }

        // POST: Paiement/Create
        [HttpPost]
        public ActionResult Create(Paiement pm)
        {
            try
            {

                // Retrieve the selected Immatricule from the posted form data
                string slectcin = Request.Form["CIN"];

                // Use the selected Immatricule and other properties to create the ConsommationCarburant object
                Paiement newpm = new Paiement
                {
                    ID_Paiements = pm.ID_Paiements,
                    CIN = slectcin,
                    Mois = pm.Mois,
                    Montant = pm.Montant
                };

                db.Paiements.Add(newpm);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Paiement/Edit/5
        public ActionResult Edit(int id)
        {
            Paiement c = db.Paiements.Find(id);

            // Retrieve the list of Immatricule values from the Vehicules table
            var icin = db.Ouvriers.Select(v => v.CIN).ToList();

            // Pass the list of Immatricule values to the view
            ViewBag.ICIN = new SelectList(icin, c.CIN);

            return View(c);
        }

        // POST: Paiement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Paiement newPaiement)
        {
            try
            {
                // Retrieve the selected Immatricule value from the posted form data
                string selectCIN = Request.Form["CIN"];

                Paiement oldpm = db.Paiements.Find(id);

                oldpm.CIN = selectCIN;
                oldpm.Mois = newPaiement.Mois;
                oldpm.Montant = newPaiement.Montant;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Paiement/Delete/5
        public ActionResult Delete(int id)
        {
            Paiement pm = db.Paiements.Find(id);
            return View(pm);
        }

        // POST: Paiement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Paiement pm = db.Paiements.Find(id);
                db.Paiements.Remove(pm);
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
