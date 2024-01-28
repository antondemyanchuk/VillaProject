using Microsoft.AspNetCore.Mvc.Rendering;
using VillaProject_WEB.Models.DTO;
using VillaProject_WEB.Models.VM.Interface;

namespace VillaProject_WEB.Models.VM
{
	public record VillaNumberCreateVM : IVillaNumberVM
	{
		public VillaNumberCreateDTO VillaNumber { get; set; }
		public IEnumerable<SelectListItem> VillaList { get; set; }
		public VillaNumberCreateVM(IEnumerable<SelectListItem> villaList) 
		{
			this.VillaList = villaList;
		}
		public VillaNumberCreateVM()
		{
		}
	}
}
