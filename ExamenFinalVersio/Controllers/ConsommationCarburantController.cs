using ExamenFinalVersio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenFinalVersio.Controllers
{
    [Authorize]
    public class ConsommationCarburantController : Controller
    {

        public static db_ProjectEleEntities db = new db_ProjectEleEntities();

        // GET: ConsommationCarburant
        public ActionResult Index()
        {
            IList<ConsommationCarburant> o = db.ConsommationCarburants.ToList();
            return View(o);
        }

        // GET: ConsommationCarburant/Details/5
        public ActionResult Details(int id)
        {
            ConsommationCarburant c = db.ConsommationCarburants.Find(id);
            return View(c);
        }

        // GET: ConsommationCarburant/Create
        public ActionResult Create()
        {
            ConsommationCarburant model = new ConsommationCarburant();

            // Populate the dropdown list with the Immatricule values
            var immatriculeList = db.Vehicules.Select(v => v.Immatricule).ToList();
            model.ImmatriculeList = new SelectList(immatriculeList);

            return View(model);
        }

        // POST: ConsommationCarburant/Create
        [HttpPost]
        public ActionResult Create(ConsommationCarburant c)
        {
            try
            {
                // Retrieve the selected Immatricule from the posted form data
                string selectedImmatricule = Request.Form["Immatricule"];

                // Use the selected Immatricule and other properties to create the ConsommationCarburant object
                ConsommationCarburant newConsommation = new ConsommationCarburant
                {
                    ID_ConsommationCarburant = c.ID_ConsommationCarburant,
                    Immatricule = selectedImmatricule,
                    VolumeGasoil = c.VolumeGasoil,
                    PrixBon = c.PrixBon,
                    DateRemplissage = c.DateRemplissage,
                    Kilometrage = c.Kilometrage
                };

                db.ConsommationCarburants.Add(newConsommation);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ConsommationCarburant/Edit/5
        public ActionResult Edit(int id)
        {
            ConsommationCarburant c = db.ConsommationCarburants.Find(id);

            // Retrieve the list of Immatricule values from the Vehicules table
            var immatriculeList = db.Vehicules.Select(v => v.Immatricule).ToList();

            // Pass the list of Immatricule values to the view
            ViewBag.ImmatriculeList = new SelectList(immatriculeList, c.Immatricule);

            return View(c);
        }

        // POST: ConsommationCarburant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ConsommationCarburant Newconsom)
        {
            try
            {
                // Retrieve the selected Immatricule value from the posted form data
                string selectedImmatricule = Request.Form["Immatricule"];

                ConsommationCarburant oldc = db.ConsommationCarburants.Find(id);
                oldc.PrixBon = Newconsom.PrixBon;
                oldc.Immatricule = selectedImmatricule;
                oldc.DateRemplissage = Newconsom.DateRemplissage;
                oldc.VolumeGasoil = Newconsom.VolumeGasoil;
                oldc.Kilometrage = Newconsom.Kilometrage;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ConsommationCarburant/Delete/5
        public ActionResult Delete(int id)
        {
            ConsommationCarburant c = db.ConsommationCarburants.Find(id);
            return View(c);
        }

        // POST: ConsommationCarburant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ConsommationCarburant c = db.ConsommationCarburants.Find(id);
                db.ConsommationCarburants.Remove(c);
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
