using MotorcycleRentals.src.Domain.Enums;

namespace MotorcycleRentals.src.Application.DTOs
{
    public class RentalCreateDto
    {
        public Guid DeliveryManId { get; set; }
        public Guid MotorcycleId { get; set; }
        public RentalPlanType PlanType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PredictedEndDate { get; set; }
    }
}
