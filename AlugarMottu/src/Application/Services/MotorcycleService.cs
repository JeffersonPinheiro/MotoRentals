using MotorcycleRentals.src.Application.DTOs;
using MotorcycleRentals.src.Application.Interfaces;
using MotorcycleRentals.src.Domain.Entities;
using MotorcycleRentals.src.Infrastructure.Persistence.Repositories;

namespace MotorcycleRentals.src.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public MotorcycleService(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<MotorcycleDto> CreateMotorcycleAsync(MotorcycleCreateDto dto)
        {
            if (await _motorcycleRepository.PlateExistsAsync(dto.Plate))
                throw new Exception("Plate already exists");

            var entity = new Motorcycle
            {
                Id = Guid.NewGuid(),
                Year = dto.Year,
                Model = dto.Model,
                Plate = dto.Plate,
            };
            await _motorcycleRepository.InsertAsync(entity);

            return new MotorcycleDto
            {
                Id = entity.Id,
                Year = entity.Year,
                Model = entity.Model,
                Plate = entity.Plate
            };
        }

        public async Task<IEnumerable<MotorcycleDto>> GetMotorcyclesAsync(string? plateFilter = null)
        {
            IEnumerable<Motorcycle> entities;
            if (string.IsNullOrWhiteSpace(plateFilter))
                entities = await _motorcycleRepository.GetAllAsync();
            else
                entities = await _motorcycleRepository.GetByPlateFilterAsync(plateFilter);

            return entities.Select(entity => new MotorcycleDto
            {
                Id = entity.Id,
                Year = entity.Year,
                Model = entity.Model,
                Plate = entity.Plate
            });
        }

        public async Task<MotorcycleDto?> GetByIdAsync(Guid id)
        {
            var entity = await _motorcycleRepository.GetByIdAsync(id);
            if (entity == null) return null;
            return new MotorcycleDto
            {
                Id = entity.Id,
                Year = entity.Year,
                Model = entity.Model,
                Plate = entity.Plate
            };
        }

        public async Task<MotorcycleDto?> UpdatePlateAsync(Guid id, MotorcycleUpdatePlateDto dto)
        {
            var entity = await _motorcycleRepository.GetByIdAsync(id);
            if (entity == null) return null;
            if (await _motorcycleRepository.PlateExistsAsync(dto.Plate))
                throw new Exception("Plate already exists");

            entity.Plate = dto.Plate;
            await _motorcycleRepository.UpdateAsync(id, entity);

            return new MotorcycleDto
            {
                Id = entity.Id,
                Year = entity.Year,
                Model = entity.Model,
                Plate = entity.Plate
            };
        }

        public async Task<bool> DeleteMotorcycleAsync(Guid id)
        {
            if (await _motorcycleRepository.HasActiveRentalAsync(id))
                throw new Exception("Cannot delete motorcycle with active rentals");

            await _motorcycleRepository.DeleteAsync(id);
            return true;
        }
    }
}
