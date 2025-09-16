using MotorcycleRentals.src.Domain.Entities;

namespace MotorcycleRentals.src.Infrastructure.Persistence.Repositories
{
    public interface IRentalRepository : IMongoRepository<Rental>
    {
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Rental>> GetRentalsByDeliveryManAsync(Guid deliveryManId);
        Task<IEnumerable<Rental>> GetRentalsByMotorcycleAsync(Guid motorcycleId);
    }
}
