using MotorcycleRentals.src.Application.DTOs;
using MotorcycleRentals.src.Application.Interfaces;
using MotorcycleRentals.src.Domain.Entities;
using MotorcycleRentals.src.Infrastructure.Persistence.Repositories;

namespace MotorcycleRentals.src.Application.Services
{
    public class DeliveryManService : IDeliveryManService
    {
        private readonly IDeliveryManRepository _deliveryManRepository;

        public DeliveryManService(IDeliveryManRepository deliveryManRepository)
        {
            _deliveryManRepository = deliveryManRepository;
        }

        public async Task<DeliveryManDto> RegisterDeliveryManAsync(DeliveryManCreateDto dto)
        {
            if (await _deliveryManRepository.CnpjExistsAsync(dto.Cnpj))
                throw new Exception("CNPJ already exists");

            if (await _deliveryManRepository.CnhNumberExistsAsync(dto.CnhNumber))
                throw new Exception("CNH number already exists");

            var entity = new DeliveryMan
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Cnpj = dto.Cnpj,
                BirthDate = dto.BirthDate,
                CnhNumber = dto.CnhNumber,
                CnhType = dto.CnhType
            };

            await _deliveryManRepository.InsertAsync(entity);

            return new DeliveryManDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Cnpj = entity.Cnpj,
                BirthDate = entity.BirthDate,
                CnhNumber = entity.CnhNumber,
                CnhType = entity.CnhType,
                CnhImagePath = entity.CnhImagePath
            };
        }

        public async Task<DeliveryManDto?> GetByIdAsync(Guid id)
        {
            var entity = await _deliveryManRepository.GetByIdAsync(id);
            if (entity == null) return null;
            return new DeliveryManDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Cnpj = entity.Cnpj,
                BirthDate = entity.BirthDate,
                CnhNumber = entity.CnhNumber,
                CnhType = entity.CnhType,
                CnhImagePath = entity.CnhImagePath
            };
        }

        public async Task<DeliveryManDto?> UpdateCnhImageAsync(Guid id, DeliveryManCnhImageDto dto)
        {
            var entity = await _deliveryManRepository.GetByIdAsync(id);
            if (entity == null) return null;

            // Salvar imagem no caminho desejado (exemplo: storage local)
            var extension = System.IO.Path.GetExtension(dto.ImageFile.FileName);
            var fileName = $"cnh_{id}_{DateTime.UtcNow.Ticks}{extension}";
            var filePath = System.IO.Path.Combine("storage", fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await dto.ImageFile.CopyToAsync(stream);
            }

            entity.CnhImagePath = filePath;
            await _deliveryManRepository.UpdateAsync(id, entity);

            return new DeliveryManDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Cnpj = entity.Cnpj,
                BirthDate = entity.BirthDate,
                CnhNumber = entity.CnhNumber,
                CnhType = entity.CnhType,
                CnhImagePath = entity.CnhImagePath
            };
        }
    }
}
