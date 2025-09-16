namespace MotorcycleRentals.src.Domain.Entities
{
    public class MotorcycleNotification
    {
        public Guid Id { get; set; }
        public Guid MotorcycleId { get; set; }
        public string Plate { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateTime NotifiedAt { get; set; }
    }
}
