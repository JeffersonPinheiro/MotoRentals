using MotorcycleRentals.src.Application.DTOs;

namespace MotorcycleRentals.src.Application.Interfaces
{
    public interface IRentalService
    {
        Task<RentalDto> CreateRentalAsync(RentalCreateDto dto);
        Task<RentalDto?> GetByIdAsync(Guid id);
        Task<RentalDto?> ReturnRentalAsync(RentalReturnDto dto);
    }
}
