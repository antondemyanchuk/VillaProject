using System.ComponentModel.DataAnnotations;

namespace VillaProject_API.Models.DTO
{
    public record VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public int Occupancy { get; set; }

        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
    }
}
