using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_bibli.Models
{
	public class Livre
	{
		public int ID { get; set; }
		[Required]
		public string Nom { get; set; }
		[Required]
		public DateTime DateParution { get; set; }
		[Required]
		public Auteur Auteur { get; set; }
	}
}