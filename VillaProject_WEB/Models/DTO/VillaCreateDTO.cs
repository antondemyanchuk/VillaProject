using System.ComponentModel.DataAnnotations;

namespace VillaProject_WEB.Models.DTO
{
    public record VillaCreateDTO
    {
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }
        public string? Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int? Sqft { get; set; }
        public int? Occupancy { get; set; }
        public string? ImageUrl { get; set; }
        public string? Amenity { get; set; }
    }
}
