using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_bibli.Models;

namespace e_bibli.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		/*
		 *  ROUTES POSSIBLES AVEC CETTE METHODE :
		 *  a) Add/Auteur/Nom/Prenom
		 *  b) Add/Client/Nom/Email/MotDePasse
		 *  c) Add/Emprunt/IDClient/IDLivre/Date-Emprunt
		 *  d) Add/Livre/Nom/Date-Parution/IDAuteur
		 *  
		 *  Permet d'ajouter un élément dans la DB
		 *  CECI N'EST DONC EVIDEMMENT PAS SECURISE ET NE DEVRAIT JAMAIS ETRE UTILISE EN PRODUCTION
		 */
		public ActionResult Add(string Table, string Entries)
		{
			if (Table != null && Entries != null)
			{
				string[] TabEntries = Entries.Split('/');
				switch (Table)
				{
					case "Auteur":
						if (TabEntries.Length == 2)
						{
							string nom = "";
							string prenom = "";
							string[] Tabnoms = TabEntries[0].Split('_');
							string[] tabprenoms = TabEntries[1].Split('_');
							foreach (string Tabnom in Tabnoms)
								nom += Tabnom + " ";
							foreach (string Tabprenom in tabprenoms)
								prenom += Tabprenom + " ";
							nom.Remove(nom.Length - 1);
							prenom.Remove(prenom.Length - 1);
							Dal dal = new Dal();
							dal.AjouterAuteur(nom, prenom);
							dal.Dispose();
						}
						else
							return View("Error");
						break;

					case "Client":
						if (TabEntries.Length == 3)
						{
							Dal dal = new Dal();
							dal.AjouterClient(TabEntries[0], TabEntries[1], TabEntries[2]);
							dal.Dispose();
						}
						else
							return View("Error");
						break;

					case "Emprunt":
						if (TabEntries.Length == 3)
						{
							if (int.TryParse(TabEntries[0], out int IDClient) && int.TryParse(TabEntries[1], out int IDLivre) && DateTime.TryParse(TabEntries[2], out DateTime DateEmprunt))
							{
								Dal dal = new Dal();
								dal.AjouterEmprunt(IDClient, IDLivre, DateEmprunt);
								dal.Dispose();
							}
							else
								return View("Error");
						}
						else
							return View("Error");
						break;

					case "Livre":
						if (TabEntries.Length == 3)
						{
							if (DateTime.TryParse(TabEntries[1], out DateTime DateParution) && int.TryParse(TabEntries[2], out int IDauteur))
							{
								string nom = "";
								string[] Tabnoms = TabEntries[0].Split('_');
								foreach (string Tabnom in Tabnoms)
									nom += Tabnom + " ";
								Dal dal = new Dal();
								dal.AjouterLivre(nom, DateParution, IDauteur);
								dal.Dispose();
							}
							else
							{
								return View("Error");
							}
						}
						else
							return View("Error");
						break;

					default: return View("Error");
				}
				ViewBag.Message = "L'item a bien été ajouté.";
				return View("Index");
			}
			else
				return View("Error");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}