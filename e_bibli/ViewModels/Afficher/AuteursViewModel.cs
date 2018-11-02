using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e_bibli.Models;

namespace e_bibli.ViewModels.Afficher
{
	public class AuteursViewModel
	{
		public List<Auteur> Auteurs;
		public AuteursViewModel()
		{
			Dal dal = new Dal();
			Auteurs = dal.ObtientTousLesAuteurs();
			dal.Dispose();
		}
	}
}