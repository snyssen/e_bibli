using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace e_bibli.Models
{
	public class Auteur
	{
		public int ID { get; set; }
		[Required]
		public string Nom { get; set; }
		[Required]
		public string Prenom { get; set; }
	}
}