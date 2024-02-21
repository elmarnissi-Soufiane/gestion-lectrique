using ExamenFinalVersio.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenFinalVersio.Controllers
{

    [Authorize]
    public class TravauxController : Controller
    {

        public static db_ProjectEleEntities db = new db_ProjectEleEntities();
        // GET: Travaux
        public ActionResult Index()
        {
            IList<Travaux> tr = db.Travauxes.ToList();
            return View(tr);
        }

        // GET: Travaux/Details/5
        public ActionResult Details(int id)
        {
            Travaux tr = db.Travauxes.Find(id);
            return View(tr);
        }

        // GET: Travaux/Create
        public ActionResult Create()
        {
            Travaux model = new Travaux();

            // Populate the dropdown list with the CIN values
            var icin = db.Ouvriers.Select(v => v.CIN).ToList();
            model.ICIN = new SelectList(icin);

            // Populate the dropdown list with the NumeroPylone values
            var inumeroPl = db.Pylones.Select(v => v.NumeroPylone).ToList();
            model.NumeroPyloneList = new SelectList(inumeroPl);


            // Populate the dropdown list for TauxAvancement with the available options
            var TauxAvancementOptions = new List<decimal> { 0.5m, 0.25m, 0.125m };
            model.TauxAvancementList = new SelectList(TauxAvancementOptions);

            return View(model);

        }

        // POST: Travaux/Create
        [HttpPost]
        public ActionResult Create(Travaux tr)
        {
            try
            {
                string selectedCIN = Request.Form["CIN"];
                string selectedNumeroPylone = Request.Form["NumeroPylone"];
                string selectedLigneTauxAvancement = Request.Form["TauxAvancement"];

                Travaux newTravaux = new Travaux
                {
                    ID_Travaux = tr.ID_Travaux,
                    NumeroPylone = Int32.Parse(selectedNumeroPylone),
                    CIN = selectedCIN,
                    DateTravail = tr.DateTravail,
                    TauxAvancement = Convert.ToDecimal(selectedLigneTauxAvancement)
                };

                db.Travauxes.Add(newTravaux);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Travaux/Edit/5
        public ActionResult Edit(int id)
        {
            Travaux tr = db.Travauxes.Find(id);

            var numeroPyloneList = db.Pylones.Select(v => new SelectListItem
            {
                Value = v.NumeroPylone.ToString(),
                Text = v.NumeroPylone.ToString()
            });

            ViewBag.NumeroPyloneList = new SelectList(numeroPyloneList, "Value", "Text", tr.NumeroPylone);

            var icinList = db.Ouvriers.Select(v => new SelectListItem
            {
                Value = v.CIN,
                Text = v.CIN
            });

            ViewBag.ICIN = new SelectList(icinList, "Value", "Text", tr.CIN);

            var tauxAvancementList = new List<SelectListItem>
            {
                new SelectListItem { Value = "0.5", Text = "50%" },
                new SelectListItem { Value = "0.25", Text = "25%" },
                new SelectListItem { Value = "0.125", Text = "12.5%" }
            };

            ViewBag.TauxAvancementList = new SelectList(tauxAvancementList, "Value", "Text", tr.TauxAvancement);

            return View(tr);

        }

        // POST: Travaux/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Travaux newTravaux)
        {
            try
            {

                string selectedCIN = Request.Form["CIN"];
                string selectedNumeroPylone = Request.Form["NumeroPylone"];
                string selectedLigneTauxAvancement = Request.Form["TauxAvancement"];

                Travaux oldTravaux = db.Travauxes.Find(id);

                oldTravaux.NumeroPylone = Int32.Parse(selectedNumeroPylone);
                oldTravaux.CIN = selectedCIN;
                oldTravaux.DateTravail = newTravaux.DateTravail;
                oldTravaux.TauxAvancement = Convert.ToDecimal(selectedLigneTauxAvancement);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Travaux/Delete/5
        public ActionResult Delete(int id)
        {
            Travaux tr = db.Travauxes.Find(id);
            return View(tr);
        }

        // POST: Travaux/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Travaux tr = db.Travauxes.Find(id);
                db.Travauxes.Remove(tr);
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
