using ExamenFinalVersio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenFinalVersio.Controllers
{

    [Authorize]
    public class PyloneController : Controller
    {

        public static db_ProjectEleEntities db = new db_ProjectEleEntities();

        // GET: Pylone
        public ActionResult Index()
        {
            IList<Pylone> pl = db.Pylones.ToList();
            return View(pl);
        }

        // GET: Pylone/Details/5
        public ActionResult Details(int id)
        {
            Pylone pl = db.Pylones.Find(id);
            return View(pl);
        }

        // GET: Pylone/Create
        public ActionResult Create()
        {
            Pylone model = new Pylone();

            // Populate the dropdown list for LigneElectrique with the available options
            var ligneElectriqueOptions = new List<string> { "60KV", "225KV", "400KV" };
            model.LigneElectriqueList = new SelectList(ligneElectriqueOptions);

            // Populate the dropdown list for EtatDegradation with the available options
            var etatDegradationOptions = new List<string> { "Bon", "Dégradé" };
            model.EtatDegradationList = new SelectList(etatDegradationOptions);

            return View(model);
        }

        // POST: Pylone/Create
        [HttpPost]
        public ActionResult Create(Pylone pl)
        {
            try
            {
                // Use the selected LigneElectrique and EtatDegradation values
                string selectedLigneElectrique = Request.Form["LigneElectrique"];
                string selectedEtatDegradation = Request.Form["EtatDegradation"];

                // Use the selected values and other properties to create the Pylone object
                Pylone newPylone = new Pylone
                {
                    NumeroPylone = pl.NumeroPylone,
                    LigneElectrique = selectedLigneElectrique,
                    EtatDegradation = selectedEtatDegradation,
                    Ville = pl.Ville,
                    Longitude = pl.Longitude,
                    Latitude = pl.Latitude
                };

                db.Pylones.Add(newPylone);
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pylone/Edit/5
        public ActionResult Edit(int id)
        {
            Pylone o = db.Pylones.Find(id);

            // Populate the dropdown list for LigneElectrique with the available options
            var ligneElectriqueOptions = new List<string> { "60KV", "225KV", "400KV" };
            ViewBag.LigneElectriqueList = new SelectList(ligneElectriqueOptions, o.LigneElectrique);

            // Populate the dropdown list for EtatDegradation with the available options
            var etatDegradationOptions = new List<string> { "Bon", "Dégradé" };
            ViewBag.EtatDegradationList = new SelectList(etatDegradationOptions, o.EtatDegradation);

            return View(o);

        }

        // POST: Pylone/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Pylone plnew)
        {
            try
            {
                // Retrieve the selected LigneElectrique and EtatDegradation values from the posted form data
                string selectedLigneElectrique = Request.Form["LigneElectrique"];
                string selectedEtatDegradation = Request.Form["EtatDegradation"];

                Pylone oldPylone = db.Pylones.Find(id);
                oldPylone.Longitude = plnew.Longitude;
                oldPylone.Ville = plnew.Ville;
                oldPylone.LigneElectrique = selectedLigneElectrique;
                oldPylone.Latitude = plnew.Latitude;
                oldPylone.EtatDegradation = selectedEtatDegradation;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pylone/Delete/5
        public ActionResult Delete(int id)
        {
            Pylone pl = db.Pylones.Find(id);
            return View(pl);
        }

        // POST: Pylone/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pylone pl = db.Pylones.Find(id);
                db.Pylones.Remove(pl);
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
