using Microsoft.OpenApi.Any;
using MongoDB.Bson.Serialization.Attributes;
using MotorcycleRentals.src.Domain.Enums;

namespace MotorcycleRentals.src.Domain.Entities
{
    public class DeliveryMan
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("cnpj")]
        public string Cnpj { get; set; }

        [BsonElement("birth_date")]
        public DateTime BirthDate { get; set; }

        [BsonElement("cnh_number")]
        public string CnhNumber { get; set; }

        [BsonElement("cnh_type")]
        public CNHType CnhType { get; set; }

        [BsonElement("cnh_image_path")]
        public string? CnhImagePath { get; set; }
    }
}
