using ExamenFinalVersio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenFinalVersio.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        db_ProjectEleEntities db = new db_ProjectEleEntities();

        // GET: Home
        public ActionResult Index()
        {
            IList<Pylone> pl = db.Pylones.ToList();
            return View(pl);
        }

        // Get 
        /*public ActionResult Search(string query)
        {
            var pylones = db.Pylones.Where(p => p.Ville.Contains(query)).ToList();

            var ouvriers = db.Ouvriers.Where(o => o.NomComplet.Contains(query)).ToList();

            var travaux = db.Travauxes.Where(t => t.DateTravail.ToString().Contains(query)).ToList();

            var paiements = db.Paiements.Where(p => p.Mois.ToString().Contains(query)).ToList();

            var vehicules = db.Vehicules.Where(v => v.Model.Contains(query)).ToList();

            var consommations = db.ConsommationCarburants.Where(c => c.PrixBon.ToString().Contains(query)).ToList();

            var repos = db.Repos.Where(r => r.MotifRepos.Contains(query)).ToList();

            // Combine the search results into a single list
            var searchResults = pylones.Concat(ouvriers).Concat(travaux)
                                       .Concat(paiements).Concat(vehicules)
                                       .Concat(consommations).Concat(repos);

            return View(searchResults);
        }*/

        /*public ActionResult Search(string attribute, string query)
        {
            object searchResults = null;

            switch (attribute)
            {
                case "EtatDegradation":
                    searchResults = db.Pylones.Where(p => p.EtatDegradation.Contains(query)).ToList();
                    break;
                case "NomComplet":
                    searchResults = db.Ouvriers.Where(o => o.NomComplet.Contains(query)).ToList();
                    break;
                case "DateTravail":
                    searchResults = db.Travauxes.Where(t => t.DateTravail.ToString().Contains(query)).ToList();
                    break;
                case "ID_Paiements":
                    searchResults = db.Paiements.Where(p => p.ID_Paiements.ToString().Contains(query)).ToList();
                    break;
                case "Immatricule":
                    searchResults = db.Vehicules.Where(v => v.Immatricule.Contains(query)).ToList();
                    break;
                case "VolumeGasoil":
                    searchResults = db.ConsommationCarburants.Where(c => c.VolumeGasoil.ToString().Contains(query)).ToList();
                    break;
                case "MotifRepos":
                    searchResults = db.Repos.Where(r => r.MotifRepos.Contains(query)).ToList();
                    break;
                default:
                    break;
            }

            if (searchResults == null || ((List<object>)searchResults).Count == 0)
            {
                ViewBag.Message = "No search results found.";
            }

            return View(searchResults);
        }*/

        public ActionResult Search(string table, string query)
        {
            var searchResults = new List<object>();

            switch (table)
            {
                case "Pylones":
                    searchResults = db.Pylones.Where(p => p.EtatDegradation.Contains(query)).Cast<object>().ToList();
                    return View("Index", searchResults.Cast<Pylone>());

                case "Ouvriers":
                    searchResults = db.Ouvriers.Where(o => o.NomComplet.Contains(query)).Cast<object>().ToList();
                    return View("Index", searchResults.Cast<Ouvrier>());

                case "Travaux":
                    searchResults = db.Travauxes.Where(t => t.DateTravail.ToString().Contains(query)).Cast<object>().ToList();
                    return View("Index", searchResults.Cast<Travaux>());

                case "Paiements":
                    searchResults = db.Paiements.Where(p => p.ID_Paiements.ToString().Contains(query)).Cast<object>().ToList();
                    return View("Index", searchResults.Cast<Paiement>());

                case "Vehicules":
                    searchResults = db.Vehicules.Where(v => v.Immatricule.Contains(query)).Cast<object>().ToList();
                    return View("Index", searchResults.Cast<Vehicule>());

                case "ConsommationCarburant":
                    searchResults = db.ConsommationCarburants.Where(c => c.VolumeGasoil.ToString().Contains(query)).Cast<object>().ToList();
                    return View("Index", searchResults.Cast<ConsommationCarburant>());

                case "Repos":
                    searchResults = db.Repos.Where(r => r.MotifRepos.Contains(query)).Cast<object>().ToList();
                    return View("Index", searchResults.Cast<Repos>());

                default:
                    return RedirectToAction("Index", "Home");
            }
        }
    }
}