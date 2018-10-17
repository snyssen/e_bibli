using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_bibli.Models
{
	public class Emprunt
	{
		[Key]
		public int ID { get; set; }
		[Required]
		public int IDClient { get; set; }
		[Required]
		public int IDLivre { get; set; }
		[Required]
		public DateTime DateEmprunt { get; set; }
		public DateTime DateRetour { get; set; }
	}
}