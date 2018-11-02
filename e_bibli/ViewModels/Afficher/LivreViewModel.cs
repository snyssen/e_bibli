using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e_bibli.Models;

namespace e_bibli.ViewModels.Afficher
{
	public class LivreViewModel
	{
		public readonly Livre livre;
		public readonly Auteur auteur;
		public readonly Client emprunteurActuel;
		public readonly List<Client> emprunteursPasses;
		public readonly bool LivreExiste;
		public LivreViewModel(int IDlivre)
		{
			Dal dal = new Dal();
			if (LivreExiste = dal.LivreExiste(IDlivre))
			{
				livre = dal.ObtenirLivre(IDlivre);
				auteur = dal.ObtenirAuteur(livre.IDAuteur);
				List<Emprunt> emprunts = dal.ObtenirEmpruntsLivre(IDlivre);
				if (emprunts != null)
				{
					//int IDemprunteurActuel = emprunts.First(emprunt => emprunt.DateRetour == null).IDClient;
					//emprunteurActuel = dal.ObtenirClient(IDemprunteurActuel);
					emprunteursPasses = new List<Client>();
					foreach (Emprunt emprunt in emprunts)
					{
						if (emprunt.DateRetour != null)
						{
							if (emprunteursPasses.Where(client => client.ID == emprunt.ID).Count() == 0)
								emprunteursPasses.Add(dal.ObtenirClient(emprunt.IDClient));
						}
						else
							emprunteurActuel = dal.ObtenirClient(emprunt.IDClient);
					}
				}
			}
		}
	}
}