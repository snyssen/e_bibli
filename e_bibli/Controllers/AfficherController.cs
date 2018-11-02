using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_bibli.ViewModels.Afficher;

namespace e_bibli.Controllers
{
    public class AfficherController : Controller
    {
        // GET: Afficher
		  // Affiche la liste des livres et leurs auteurs
        public ActionResult Index()
        {
				IndexViewModel vm = new IndexViewModel();
            return View(vm);
        }

		// GET: Afficher/Auteurs
		// Affiche la liste des auteurs
		public ActionResult Auteurs()
		{
			AuteursViewModel vm = new AuteursViewModel();
			return View(vm);
		}

		// GET: Afficher/Auteur/IDauteur
		// Affiche l'auteur et tous les livres qu'il a écrit
		public ActionResult Auteur(int IDauteur)
		{
			if (IDauteur != 0)
			{
				AuteurViewModel vm = new AuteurViewModel(IDauteur);
				if (vm.auteurExiste)
					return View(vm);
				else
					return View("Error");
			}
			else
				return View("Error");
		}

		// GET: Afficher/Livre/IDlivre
		// Affiche un livre, son auteur, l'emprunteur actuel et la liste des emprunteurs passés
		public ActionResult Livre(int IDlivre)
		{
			LivreViewModel vm = new LivreViewModel(IDlivre);
			if (vm.LivreExiste)
				return View(vm);
			else
				return View("Error");
		}
    }
}