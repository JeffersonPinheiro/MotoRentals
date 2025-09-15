using System.ComponentModel.DataAnnotations;

namespace MotorcycleRentals.src.Application.DTOs
{
    public class UploadCnhRequest
    {
        [Required]
        public IFormFile CnhFile { get; set; }
    }
}
