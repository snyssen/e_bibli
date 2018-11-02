using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace e_bibli
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// Affiche l'auteur et tous les livres qu'il a écrit
			routes.MapRoute(
				name: "Afficher_Auteur",
				url: "Afficher/Auteur/{IDauteur}/{*Args}",
				defaults: new { Controller = "Afficher", action = "Auteur", IDauteur = 0, Args = UrlParameter.Optional }
			);

			// Affiche un livre, son auteur, l'emprunteur actuel et la liste des emprunteurs passés
			routes.MapRoute(
				name: "Afficher_Livre",
				url: "Afficher/Livre/{IDlivre}/{*Args}",
				defaults: new { Controller = "Afficher", action = "Livre", IDlivre = 0, Args = UrlParameter.Optional}
			);

			// Affiche la liste des auteurs
			routes.MapRoute(
				name: "Afficher_Auteurs",
				url: "Afficher/Auteurs/{*Args}",
				defaults: new { controller = "Afficher", action = "Auteurs", Args = UrlParameter.Optional }
			);

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
			routes.MapRoute(
				name: "Add",
				url: "Add/{Table}/{*Entries}",
				defaults: new { controller = "Home", action = "Add"}
			);

			routes.MapRoute(
				name: "Afficher",
				url: "Afficher/{*Args}",
				defaults: new { Controller = "Afficher", action = "Index", Args = UrlParameter.Optional }
			);

			routes.MapRoute(
				 name: "Default",
				 url: "",
				 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
