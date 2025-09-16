using System.ComponentModel.DataAnnotations;
using MotorcycleRentals.src.Domain.Enums;

namespace MotorcycleRentals.src.Application.DTOs
{
    public class DeliveryManCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "CNPJ must have 14 digits")]
        public string Cnpj { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CNH must have 11 digits")]
        public string CnhNumber { get; set; }

        [Required]
        [EnumDataType(typeof(CNHType))]
        public CNHType CnhType { get; set; }
    }
}
