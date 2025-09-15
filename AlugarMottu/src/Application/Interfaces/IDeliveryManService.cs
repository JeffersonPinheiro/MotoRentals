using MotorcycleRentals.src.Application.DTOs;
using MotorcycleRentals.src.Domain.Entities;

namespace MotorcycleRentals.src.Application.Interfaces
{
    public interface IDeliveryManService
    {
        Task<DeliveryManDto> RegisterDeliveryManAsync(DeliveryManCreateDto dto);
        Task<DeliveryManDto?> GetByIdAsync(Guid id);
        Task<DeliveryManDto?> UpdateCnhImageAsync(Guid id, DeliveryManCnhImageDto dto);
    }
}

