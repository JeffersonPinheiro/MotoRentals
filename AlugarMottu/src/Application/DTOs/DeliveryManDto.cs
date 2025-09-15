using MotorcycleRentals.src.Domain.Enums;

namespace MotorcycleRentals.src.Application.DTOs
{
    public class DeliveryManDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public CNHType CnhType { get; set; }
        public string? CnhImagePath { get; set; }
    }
}
