using MongoDB.Bson.Serialization.Attributes;

namespace MotorcycleRentals.src.Domain.Entities
{
    public class Motorcycle
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("model")]
        public string Model { get; set; }

        [BsonElement("plate")]
        public string Plate { get; set; }
    }
}
