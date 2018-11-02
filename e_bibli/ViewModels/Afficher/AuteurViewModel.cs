using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e_bibli.Models;

namespace e_bibli.ViewModels.Afficher
{
	public class AuteurViewModel
	{
		public readonly Auteur auteur;
		public readonly List<Livre> livres;
		public bool auteurExiste;
		public AuteurViewModel(int IDauteur)
		{
			Dal dal = new Dal();
			if (auteurExiste = dal.AuteurExiste(IDauteur))
			{
				auteur = dal.ObtenirAuteur(IDauteur);
				livres = dal.ObtenirLivresAuteur(IDauteur);
			}
			dal.Dispose();
		}
	}
}