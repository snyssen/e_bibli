using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_bibli.Models
{
	interface IDal : IDisposable
	{
		// Auteur
		List<Auteur> ObtientTousLesAuteurs();
		int AjouterAuteur(string nom, string prenom);
		Auteur ObtenirAuteur(int id);
		void ModifierAuteur(int id, string nom, string prenom);
		bool AuteurExiste(string nom);

		// Client
		List<Livre> ObtenirTousLesLivres();
		Client ObtenirClient(int id);
		Client ObtenirClient(string idStr);
		int AjouterClient(string nom, string mail, string motDePasse);
		Client Authentifier(string nom, string motDePasse);
		Client AuthentifierMail(string email, string motDePasse);

		// Livre
		int AjouterLivre(string titre, DateTime dateParution, Auteur auteur);
		Livre ObtenirLivre(int id);

		// Emprunt
		int AjouterEmprunt(int idClient, int idLivre, DateTime dateEmprunt);
		void ModifierEmprunt(int id, int idClient, int idLivre, DateTime dateEmprunt, DateTime dateRetour);
		void ModifierEmprunt(int id, int idClient, int idLivre, DateTime dateEmprunt);
		void RetournerEmprunt(int id);
		Emprunt ObtenirEmprunt(int id);
	}
}
