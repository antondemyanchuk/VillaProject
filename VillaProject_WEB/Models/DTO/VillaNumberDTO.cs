﻿using System.ComponentModel.DataAnnotations;

namespace VillaProject_WEB.Models.DTO
{
	public record VillaNumberDTO
	{
		[Required]
		public int VillaNo { get; set; }
		[Required]
		public int VillaId { get; set; }
		public string SpecialDetails { get; set; }
		public VillaDTO Villa { get; set; }
	}
}