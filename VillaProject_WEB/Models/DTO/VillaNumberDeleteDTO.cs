using System.ComponentModel.DataAnnotations;
using VillaProject_API.Models;

namespace VillaProject_WEB.Models.DTO
{
    public class VillaNumberDeleteDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
        public VillaDTO Villa { get; set; }
    }
}
