using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_bibli.Models
{
	public class Client
	{
		public int ID { get; set; }
		[Required]
		public string Nom { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string MotDePasse { get; set; }
	}
}