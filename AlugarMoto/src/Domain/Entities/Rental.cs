using MongoDB.Bson.Serialization.Attributes;
using MotorcycleRentals.src.Domain.Enums;

namespace MotorcycleRentals.src.Domain.Entities
{
    public class Rental
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("deliveryman_id")]
        public Guid DeliveryManId { get; set; }

        [BsonElement("motorcycle_id")]
        public Guid MotorcycleId { get; set; }

        [BsonElement("plan_type")]
        public RentalPlanType PlanType { get; set; }

        [BsonElement("start_date")]
        public DateTime StartDate { get; set; }

        [BsonElement("end_date")]
        public DateTime? EndDate { get; set; }

        [BsonElement("predicted_end_date")]
        public DateTime PredictedEndDate { get; set; }

        [BsonElement("total_price")]
        public decimal TotalPrice { get; set; }

        [BsonElement("status")]
        public RentalStatus Status { get; set; }
    }
}
