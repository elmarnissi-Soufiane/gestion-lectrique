using ExamenFinalVersio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenFinalVersio.Controllers
{

    [Authorize]
    public class ReposController : Controller
    {

        public static db_ProjectEleEntities db = new db_ProjectEleEntities();
        // GET: Repos
        public ActionResult Index()
        {
            IList<Repos> rp = db.Repos.ToList();
            return View(rp);
        }

        // GET: Repos/Details/5
        public ActionResult Details(int id)
        {
            Repos rp = db.Repos.Find(id);
            return View(rp);
        }

        // GET: Repos/Create
        public ActionResult Create()
        {
            Repos model = new Repos();

            // Populate the dropdown list with the Immatricule values
            var icin = db.Ouvriers.Select(v => v.CIN).ToList();
            model.ICIN = new SelectList(icin);

            return View(model);
        }

        // POST: Repos/Create
        [HttpPost]
        public ActionResult Create(Repos rp)
        {
            try
            {
                // Retrieve the selected Immatricule from the posted form data
                string slectcin = Request.Form["CIN"];

                // Use the selected Immatricule and other properties to create the ConsommationCarburant object
                Repos newrp = new Repos
                {
                    ID_Repos = rp.ID_Repos,
                    CIN = slectcin,
                    DateRepos = rp.DateRepos,
                    MotifRepos = rp.MotifRepos
                };

                db.Repos.Add(newrp);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Repos/Edit/5
        public ActionResult Edit(int id)
        {
            Repos c = db.Repos.Find(id);

            // Retrieve the list of Immatricule values from the Vehicules table
            var icin = db.Ouvriers.Select(v => v.CIN).ToList();

            // Pass the list of Immatricule values to the view
            ViewBag.ICIN = new SelectList(icin, c.CIN);

            return View(c);
        }

        // POST: Repos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Repos newRepos)
        {
            try
            {
                // Retrieve the selected Immatricule value from the posted form data
                string selectCIN = Request.Form["CIN"];

                Repos oldrp = db.Repos.Find(id);

                oldrp.CIN = selectCIN;
                oldrp.DateRepos = newRepos.DateRepos;
                oldrp.MotifRepos = newRepos.MotifRepos;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Repos/Delete/5
        public ActionResult Delete(int id)
        {
            Repos rp = db.Repos.Find(id);
            return View(rp);
        }

        // POST: Repos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Repos rp = db.Repos.Find(id);
                db.Repos.Remove(rp);
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
