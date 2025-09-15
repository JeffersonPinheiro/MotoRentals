using MotorcycleRentals.src.Domain.Entities;

namespace MotorcycleRentals.src.Infrastructure.Persistence.Repositories
{
    public interface IMotorcycleNotificationRepository
    {
        Task InsertNotificationAsync(MotorcycleNotification notification);
        Task<IEnumerable<MotorcycleNotification>> GetNotificationsAsync();
        Task<IEnumerable<MotorcycleNotification>> GetNotificationsByYearAsync(int year);
        Task<IEnumerable<MotorcycleNotification>> GetNotificationsByPlateAsync(string plate);
    }
}
