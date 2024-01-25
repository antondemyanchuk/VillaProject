using Microsoft.AspNetCore.Mvc.Rendering;
using VillaProject_WEB.Models.DTO;

namespace VillaProject_WEB.Models.VM
{
	public class VillaNumberCreateVM
	{
        public VillaNumberCreateDTO VillaNumber { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; }
	}
}
