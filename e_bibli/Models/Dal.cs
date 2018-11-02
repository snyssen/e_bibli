using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace e_bibli.Models
{
	public class Dal : IDal
	{
		private BddContext bdd;
		public Dal()
		{
			bdd = new BddContext();
		}
		public void Dispose()
		{
			bdd.Dispose();
		}

		// Auteur
		public List<Auteur> ObtientTousLesAuteurs()
		{
			return bdd.Auteurs.ToList();
		}
		public int AjouterAuteur(string nom, string prenom)
		{
			Auteur auteurAjoute = bdd.Auteurs.Add(new Auteur { Nom = nom, Prenom = prenom });
			bdd.SaveChanges();
			return auteurAjoute.ID;
		}
		public Auteur ObtenirAuteur(int id)
		{
			return bdd.Auteurs.FirstOrDefault(auteur => auteur.ID == id);
		}
		public Auteur ObtenirAuteur(string nom)
		{
			return bdd.Auteurs.FirstOrDefault(auteur => auteur.Nom == nom);
		}
		public void ModifierAuteur(int id, string nom, string prenom)
		{
			Auteur auteurTrouve = bdd.Auteurs.FirstOrDefault(auteur => auteur.ID == id);
			if (auteurTrouve != null)
			{
				auteurTrouve.Nom = nom;
				auteurTrouve.Prenom = prenom;
				bdd.SaveChanges();
			}
		}
		public bool AuteurExiste(string nom)
		{
			if (bdd.Auteurs.FirstOrDefault(auteur => auteur.Nom == nom) != null)
				return true;
			else
				return false;
		}
		public bool AuteurExiste(int IDauteur)
		{
			if (bdd.Auteurs.FirstOrDefault(auteur => auteur.ID == IDauteur) != null)
				return true;
			else
				return false;
		}

		// Client
		public Client ObtenirClient(int id)
		{
			return bdd.Clients.FirstOrDefault(client => client.ID == id);
		}
		public Client ObtenirClient(string idStr)
		{
			if (!int.TryParse(idStr, out int id))
				return bdd.Clients.FirstOrDefault(client => client.ID == id);
			else
				return null;
		}
		public int AjouterClient(string nom, string mail, string motDePasse)
		{
			Client clientAjoute = bdd.Clients.Add(new Client { Nom = nom, Email = mail, MotDePasse = EncodeMD5(motDePasse) });
			bdd.SaveChanges();
			return clientAjoute.ID;
		}
		public Client Authentifier(string nom, string motDePasse)
		{
			string motDePasseHashe = EncodeMD5(motDePasse);
			return bdd.Clients.FirstOrDefault(client => client.Nom == nom && client.MotDePasse == motDePasseHashe);
		}
		public Client AuthentifierMail(string email, string motDePasse)
		{
			string motDePasseHashe = EncodeMD5(motDePasse);
			return bdd.Clients.FirstOrDefault(client => client.Email == email && client.MotDePasse == motDePasseHashe);
		}

		// Livre
		public int AjouterLivre(string titre, DateTime dateParution, int IDauteur)
		{
			Livre livreAjoute = bdd.Livres.Add(new Livre { Nom = titre, DateParution = dateParution, IDAuteur = IDauteur });
			bdd.SaveChanges();
			return livreAjoute.ID;
		}
		public Livre ObtenirLivre(int id)
		{
			return bdd.Livres.FirstOrDefault(livre => livre.ID == id);
		}
		public List<Livre> ObtenirTousLesLivres()
		{
			return bdd.Livres.ToList();
		}
		public List<Livre> ObtenirLivresAuteur(int  IDauteur)
		{
			return bdd.Livres.Where(livre => livre.IDAuteur == IDauteur).ToList();
		}
		public bool LivreExiste(int IDlivre)
		{
			if (bdd.Livres.FirstOrDefault(livre => livre.ID == IDlivre) != null)
				return true;
			else
				return false;
		}


		// Emprunt
		public int AjouterEmprunt(int idClient, int idLivre, DateTime dateEmprunt)
		{
			Emprunt empruntAjoute = bdd.Emprunts.Add(new Emprunt { IDClient = idClient, IDLivre = idLivre, DateEmprunt = dateEmprunt });
			bdd.SaveChanges();
			return empruntAjoute.ID;
		}
		public void ModifierEmprunt(int id,int idClient, int idLivre, DateTime dateEmprunt, DateTime dateRetour)
		{
			Emprunt empruntTrouve = bdd.Emprunts.FirstOrDefault(emprunt => emprunt.ID == id);
			if (empruntTrouve != null)
			{
				empruntTrouve.IDClient = idClient;
				empruntTrouve.IDLivre = idLivre;
				empruntTrouve.DateEmprunt = dateEmprunt;
				empruntTrouve.DateRetour = dateRetour;
				bdd.SaveChanges();
			}
		}
		public void ModifierEmprunt(int id, int idClient, int idLivre, DateTime dateEmprunt)
		{
			Emprunt empruntTrouve = bdd.Emprunts.FirstOrDefault(emprunt => emprunt.ID == id);
			if (empruntTrouve != null)
			{
				empruntTrouve.IDClient = idClient;
				empruntTrouve.IDLivre = idLivre;
				empruntTrouve.DateEmprunt = dateEmprunt;
				bdd.SaveChanges();
			}
		}
		public void RetournerEmprunt(int id)
		{
			Emprunt empruntTrouve = bdd.Emprunts.FirstOrDefault(emprunt => emprunt.ID == id);
			if (empruntTrouve != null)
			{
				empruntTrouve.DateRetour = DateTime.Now;
				bdd.SaveChanges();
			}
		}
		public Emprunt ObtenirEmprunt(int id)
		{
			return bdd.Emprunts.FirstOrDefault(emprunt => emprunt.ID == id);
		}
		public List<Emprunt> ObtenirEmpruntsClient(int IDclient)
		{
			return bdd.Emprunts.Where(emprunt => emprunt.IDClient == IDclient).ToList();
		}
		public List<Emprunt> ObtenirEmpruntsLivre(int IDlivre)
		{
			return bdd.Emprunts.Where(emprunt => emprunt.IDLivre == IDlivre).ToList();
		}

		private string EncodeMD5(string motDePasse)
		{
			string motDePasseSel = "e_bibli" + motDePasse + "ASP.NET MVC";
			return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
		}
	}
}