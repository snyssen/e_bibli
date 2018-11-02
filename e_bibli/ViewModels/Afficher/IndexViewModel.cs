using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e_bibli.Models;

namespace e_bibli.ViewModels.Afficher
{
	public class IndexViewModel
	{
		private readonly List<Livre> livres;
		public List<LivreAut> livreAuts;
		public IndexViewModel()
		{
			Dal dal = new Dal();
			livres = dal.ObtenirTousLesLivres();
			livreAuts = new List<LivreAut>();
			foreach (Livre livre in livres)
			{
				Auteur auteur = dal.ObtenirAuteur(livre.IDAuteur);
				livreAuts.Add(new LivreAut { Livre = livre, Auteur = auteur });
			}
			dal.Dispose();
		}
	}
	public class LivreAut
	{
		public Livre Livre { get; set; }
		public Auteur Auteur { get; set; }
	}

}