using System.ComponentModel.DataAnnotations;

namespace MotorcycleRentals.src.Application.DTOs
{
    public class MotorcycleCreateDto
    {

        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{3}-\d{4}$", ErrorMessage = "Plate must be in format AAA-1234")]
        public string Plate { get; set; }
    }
}
