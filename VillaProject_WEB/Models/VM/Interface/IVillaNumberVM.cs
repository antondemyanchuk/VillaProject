using Microsoft.AspNetCore.Mvc.Rendering;

namespace VillaProject_WEB.Models.VM.Interface
{
    public interface IVillaNumberVM
    {
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
