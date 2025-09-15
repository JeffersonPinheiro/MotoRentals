using MotorcycleRentals.src.Application.DTOs;

namespace MotorcycleRentals.src.Application.Interfaces
{
    public interface IMotorcycleService
    {
        Task<MotorcycleDto> CreateMotorcycleAsync(MotorcycleCreateDto dto);
        Task<IEnumerable<MotorcycleDto>> GetMotorcyclesAsync(string? plateFilter = null);
        Task<MotorcycleDto?> GetByIdAsync(Guid id);
        Task<MotorcycleDto?> UpdatePlateAsync(Guid id, MotorcycleUpdatePlateDto dto);
        Task<bool> DeleteMotorcycleAsync(Guid id);
    }
}
