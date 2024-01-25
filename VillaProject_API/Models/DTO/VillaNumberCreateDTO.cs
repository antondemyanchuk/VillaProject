using System.ComponentModel.DataAnnotations;

namespace VillaProject_API.Models.DTO
{
    public record VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
