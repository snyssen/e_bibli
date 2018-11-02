using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using e_bibli.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace e_bibli.Tests
{
	[TestClass]
	public class DalTests
	{
		private Dal dal;

		[TestInitialize]
		public void Init_AvantChaqueTest()
		{
			IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
			Database.SetInitializer(init);
			init.InitializeDatabase(new BddContext());
			dal = new Dal();
		}

		[TestCleanup]
		public void ApresChaqueTest()
		{
			dal.Dispose();
		}

		[TestMethod]
		public void CreerAuteur_AvecUnNouvelAuteur_ObtientTousLesAuteursRenvoitBienLAuteur()
		{
			dal.AjouterAuteur("Werber", "Bernard");
			List<Auteur> auteurs = dal.ObtientTousLesAuteurs();

			Assert.IsNotNull(auteurs);
			Assert.AreEqual(1, auteurs.Count);
			Assert.AreEqual("Werber", auteurs[0].Nom);
			Assert.AreEqual("Bernard", auteurs[0].Prenom);
		}

		[TestMethod]
		public void ModifierAuteur_CreationDUnNouvelAuteurEtChangementNomEtPrenom_LaModificationEstCorrecteApresRechargement() {
			dal.AjouterAuteur("Werber", "Bernard");
			dal.ModifierAuteur(1, "Asimov", "Isaac");
			List<Auteur> auteurs = dal.ObtientTousLesAuteurs();

			Assert.IsNotNull(auteurs);
			Assert.AreEqual(1, auteurs.Count);
			Assert.AreEqual("Asimov", auteurs[0].Nom);
			Assert.AreEqual("Isaac", auteurs[0].Prenom);
		}

		[TestMethod]
		public void AuteurExiste_AvecCreationDunAuteur_RenvoiQuilExiste() {
			dal.AjouterAuteur("Werber", "Bernard");
			Assert.IsTrue(dal.AuteurExiste("Werber"));
			Assert.IsFalse(dal.AuteurExiste("Asimov"));
		}

		[TestMethod]
		public void ObtenirClient_ClientInexistant_RetourneNull() {
			Client clientInexistant = dal.ObtenirClient(15);
			Assert.IsNull(clientInexistant);
		}

		[TestMethod]
		public void ObtenirClient_IdNonNumerique_RetourneNull() {
			Client clientBadID = dal.ObtenirClient("abc");
			Assert.IsNull(clientBadID);
		}

		[TestMethod]
		public void AjouterClient_NouveauClientEtRecuperation_LeClientEstBienRecupere() {
			dal.AjouterClient("GaideMachin", "GaideMachin@student.hel.be", "1234");
			Client clientOK = dal.ObtenirClient(1);

			Assert.IsNotNull(clientOK);
			Assert.AreEqual("GaideMachin", clientOK.Nom);
			Assert.AreEqual("GaideMachin@student.hel.be", clientOK.Email);
		}

		[TestMethod]
		public void Authentifier_LoginMdpOk_AuthentificationOK() {
			dal.AjouterClient("GaideMachin", "GaideMachin@student.hel.be", "1234");
			Client clientOK = dal.Authentifier("GaideMachin", "1234");

			Assert.IsNotNull(clientOK);
			Assert.AreEqual("GaideMachin", clientOK.Nom);
			Assert.AreEqual("GaideMachin@student.hel.be", clientOK.Email);
		}

		[TestMethod]
		public void Authentifier_LoginOkMdpKo_AuthentificationKO() {
			dal.AjouterClient("GaideMachin", "GaideMachin@student.hel.be", "1234");
			Client clientKO = dal.Authentifier("GaideMachin", "4567");

			Assert.IsNull(clientKO);
		}

		[TestMethod]
		public void Authentifier_LoginKoMdpOk_AuthentificationKO() {
			dal.AjouterClient("GaideMachin", "GaideMachin@student.hel.be", "1234");
			Client clientKO = dal.Authentifier("GaideBidule", "1234");

			Assert.IsNull(clientKO);
		}

		[TestMethod]
		public void Authentifier_LoginMdpKo_AuthentificationKO() {
			dal.AjouterClient("GaideMachin", "GaideMachin@student.hel.be", "1234");
			Client clientKO = dal.Authentifier("GaideBidule", "4567");

			Assert.IsNull(clientKO);
		}

		[TestMethod]
		public void Authentifier_LoginMdpOk_AuthentificationMailOK()
		{
			dal.AjouterClient("GaideMachin", "GaideMachin@student.hel.be", "1234");
			Client clientOK = dal.AuthentifierMail("GaideMachin@student.hel.be", "1234");

			Assert.IsNotNull(clientOK);
			Assert.AreEqual("GaideMachin", clientOK.Nom);
			Assert.AreEqual("GaideMachin@student.hel.be", clientOK.Email);
		}

		[TestMethod]
		public void Authentifier_LoginOkMdpKo_AuthentificationMailKO()
		{
			dal.AjouterClient("GaideMachin", "GaideMachin@student.hel.be", "1234");
			Client clientKO = dal.AuthentifierMail("GaideMachin@student.hel.be", "4567");

			Assert.IsNull(clientKO);
		}

		[TestMethod]
		public void Authentifier_LoginKoMdpOk_AuthentificationMailKO()
		{
			dal.AjouterClient("GaideMachin", "GaideMachin@student.hel.be", "1234");
			Client clientKO = dal.Authentifier("GaideBidule@student.hel.be", "1234");

			Assert.IsNull(clientKO);
		}

		[TestMethod]
		public void Authentifier_LoginMdpKo_AuthentificationMailKO()
		{
			dal.AjouterClient("GaideMachin", "GaideMachin@student.hel.be", "1234");
			Client clientKO = dal.Authentifier("GaideBidule@student.hel.be", "4567");

			Assert.IsNull(clientKO);
		}


		[TestMethod]
		public void AjouterLivre__NouveauLivreEtRecuperation_LeLivreEstBienRecupere() {
			int idAuteur = dal.AjouterAuteur("Asimov", "Isaac");
			dal.AjouterLivre("I, Robot", DateTime.Parse("12/02/1950"), idAuteur);
			Livre livreTrouve = dal.ObtenirLivre(1);

			DateTime dateParution = DateTime.Parse("12/02/1950");
			Assert.IsNotNull(livreTrouve);
			Assert.AreEqual("I, Robot", livreTrouve.Nom);
			Assert.AreEqual(dateParution, livreTrouve.DateParution);
			Assert.AreEqual(idAuteur, livreTrouve.IDAuteur);
		}
		[TestMethod]
		public void AjouterLivres_ObtenirTousLesLivres_IlsSontBienRecupere()
		{
			int idAuteur = dal.AjouterAuteur("Asimov", "Isaac");
			dal.AjouterLivre("I, Robot", DateTime.Parse("12/02/1950"), idAuteur);
			dal.AjouterLivre("The caves of steel", DateTime.Parse("02/04/1954"), idAuteur);
			List<Livre> tousLivres = dal.ObtenirTousLesLivres();

			Assert.IsNotNull(tousLivres);
			Assert.AreEqual(tousLivres.Count, 2);
			Assert.AreEqual("I, Robot", tousLivres[0].Nom);
			Assert.AreEqual("The caves of steel", tousLivres[1].Nom);
		}
	}
}
