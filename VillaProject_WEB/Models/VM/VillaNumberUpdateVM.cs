using Microsoft.AspNetCore.Mvc.Rendering;
using VillaProject_API.Models;
using VillaProject_WEB.Models.DTO;
using VillaProject_WEB.Models.VM.Interface;

namespace VillaProject_WEB.Models.VM
{
	public record VillaNumberUpdateVM(IEnumerable<SelectListItem> villaList) : IVillaNumberVM
	{
		public VillaNumberUpdateDTO VillaNumber { get; set; }
		public SelectListItem SelectedVilla { get; set; }
		public IEnumerable<SelectListItem> VillaList { get; set; } = villaList;
	}
}
