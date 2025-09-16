using MotorcycleRentals.src.Domain.Entities;

namespace MotorcycleRentals.src.Infrastructure.Persistence.Repositories
{
    public interface IDeliveryManRepository : IMongoRepository<DeliveryMan>
    {
        Task AddAsync(DeliveryMan deliveryMan);
        Task UpdateAsync(DeliveryMan deliveryMan);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<DeliveryMan>> FilterByCnpjAsync(string cnpjPartial);
        Task<IEnumerable<DeliveryMan>> FilterByCnhNumberAsync(string cnhPartial);
        Task<DeliveryMan> GetByCnpjAsync(string cnpj);
        Task<DeliveryMan> GetByCnhNumberAsync(string cnhNumber);
        Task<bool> CnpjExistsAsync(string cnpj);
        Task<bool> CnhNumberExistsAsync(string cnhNumber);
    }
}
