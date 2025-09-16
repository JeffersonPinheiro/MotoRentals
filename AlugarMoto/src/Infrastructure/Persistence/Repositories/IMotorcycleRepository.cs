using MotorcycleRentals.src.Domain.Entities;

namespace MotorcycleRentals.src.Infrastructure.Persistence.Repositories
{
    public interface IMotorcycleRepository : IMongoRepository<Motorcycle>
    {
        Task AddAsync(Motorcycle motorcycle);
        Task UpdateAsync(Motorcycle motorcycle);
        Task DeleteAsync(Guid id);
        Task<Motorcycle> GetByPlateAsync(string plate);
        Task<IEnumerable<Motorcycle>> GetByPlateFilterAsync(string plate);
        Task<bool> PlateExistsAsync(string plate);
        Task<bool> HasActiveRentalAsync(Guid motorcycleId);
    }
}
