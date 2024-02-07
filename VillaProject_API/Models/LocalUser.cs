﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VillaProject_API.Models
{
	public class LocalUser
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
	}
}
