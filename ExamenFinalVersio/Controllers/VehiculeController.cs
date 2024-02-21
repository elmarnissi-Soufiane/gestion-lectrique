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
    public class VehiculeController : Controller
    {

        public static db_ProjectEleEntities db = new db_ProjectEleEntities();

        // GET: Vehicule
        public ActionResult Index()
        {
            IList<Vehicule> vhl = db.Vehicules.ToList();
            return View(vhl);
        }

        // GET: Vehicule/Details/5
        public ActionResult Details(String id)
        {
            Vehicule vhl = db.Vehicules.Find(id);
            return View(vhl);
        }

        // GET: Vehicule/Create
        public ActionResult Create()
        {
            Vehicule model = new Vehicule();

            var carb = new List<string> { "Essence", "Diesel" };
            model.CarburantOptions = new SelectList(carb);

            return View(model);
        }

        // POST: Vehicule/Create
        [HttpPost]
        public ActionResult Create(Vehicule vhl)
        {
            try
            {
                string selectedCarburant = Request.Form["TypeCarburant"];

                // Use the selected values and other properties to create the Pylone object
                Vehicule newVehicule = new Vehicule
                {
                    Immatricule = vhl.Immatricule,
                    Model = vhl.Model,
                    TypeCarburant = selectedCarburant,
                    KilometrageInitial = vhl.KilometrageInitial
                };

                db.Vehicules.Add(newVehicule);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicule/Edit/5
        public ActionResult Edit(String id)
        {
            Vehicule vhl = db.Vehicules.Find(id);
            // Populate the dropdown list for LigneElectrique with the available options
            var carbOptions = new List<string> { "Essence", "Diesel" };
            ViewBag.CarburantOptions = new SelectList(carbOptions, vhl.TypeCarburant);

            return View(vhl);
        }

        // POST: Vehicule/Edit/5
        [HttpPost]
        public ActionResult Edit(String id, Vehicule newVehicule)
        {
            try
            {

                string selectedCarburant = Request.Form["TypeCarburant"];

                Vehicule oldVehicule = db.Vehicules.Find(id);

                oldVehicule.Model = newVehicule.Model;
                oldVehicule.TypeCarburant = selectedCarburant;
                oldVehicule.KilometrageInitial = newVehicule.KilometrageInitial;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicule/Delete/5
        public ActionResult Delete(String id)
        {
            Vehicule vhl = db.Vehicules.Find(id);
            return View(vhl);
        }

        // POST: Vehicule/Delete/5
        [HttpPost]
        public ActionResult Delete(String id, FormCollection collection)
        {
            try
            {
                Vehicule vhl = db.Vehicules.Find(id);
                db.Vehicules.Remove(vhl);
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
