using MotorcycleRentals.src.Application.DTOs;
using MotorcycleRentals.src.Application.Interfaces;
using MotorcycleRentals.src.Domain.Entities;
using MotorcycleRentals.src.Domain.Enums;
using MotorcycleRentals.src.Infrastructure.Persistence.Repositories;

namespace MotorcycleRentals.src.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IDeliveryManRepository _deliveryManRepository;

        public RentalService(
            IRentalRepository rentalRepository,
            IMotorcycleRepository motorcycleRepository,
            IDeliveryManRepository deliveryManRepository)
        {
            _rentalRepository = rentalRepository;
            _motorcycleRepository = motorcycleRepository;
            _deliveryManRepository = deliveryManRepository;
        }

        public async Task<RentalDto> CreateRentalAsync(RentalCreateDto dto)
        {
            var deliveryMan = await _deliveryManRepository.GetByIdAsync(dto.DeliveryManId);
            if (deliveryMan == null)
                throw new Exception("DeliveryMan not found");

            if (deliveryMan.CnhType != CNHType.A && deliveryMan.CnhType != CNHType.AB)
                throw new Exception("DeliveryMan not authorized for this rental");

            var motorcycle = await _motorcycleRepository.GetByIdAsync(dto.MotorcycleId);
            if (motorcycle == null)
                throw new Exception("Motorcycle not found");

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                DeliveryManId = dto.DeliveryManId,
                MotorcycleId = dto.MotorcycleId,
                PlanType = dto.PlanType,
                StartDate = dto.StartDate,
                PredictedEndDate = dto.PredictedEndDate,
                Status = RentalStatus.Active,
                TotalPrice = CalculateRentalPrice(dto.PlanType, (dto.PredictedEndDate - dto.StartDate).Days)
            };

            await _rentalRepository.InsertAsync(rental);

            return ToDto(rental);
        }

        public async Task<RentalDto?> GetByIdAsync(Guid id)
        {
            var entity = await _rentalRepository.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<RentalDto?> ReturnRentalAsync(RentalReturnDto dto)
        {
            var rental = await _rentalRepository.GetByIdAsync(dto.RentalId);
            if (rental == null) return null;

            rental.EndDate = dto.ReturnDate;
            rental.TotalPrice = CalculateReturnPrice(rental, dto.ReturnDate);
            rental.Status = RentalStatus.Finished;

            await _rentalRepository.UpdateAsync(dto.RentalId, rental);
            return ToDto(rental);
        }

        private decimal CalculateRentalPrice(RentalPlanType planType, int days)
        {
            decimal daily = planType switch
            {
                RentalPlanType.SevenDays => 30,
                RentalPlanType.FifteenDays => 28,
                RentalPlanType.ThirtyDays => 22,
                RentalPlanType.FortyFiveDays => 20,
                RentalPlanType.FiftyDays => 18,
                _ => 30
            };
            return daily * days;
        }

        private decimal CalculateReturnPrice(Rental rental, DateTime returnDate)
        {
            int planDays = (int)rental.PlanType;
            decimal daily = CalculateRentalPrice(rental.PlanType, 1);
            int usedDays = (returnDate - rental.StartDate).Days;

            if (returnDate < rental.PredictedEndDate)
            {
                int notUsedDays = (rental.PredictedEndDate - returnDate).Days;
                decimal basePrice = daily * usedDays;
                decimal notUsedPrice = daily * notUsedDays;
                decimal multa = rental.PlanType switch
                {
                    RentalPlanType.SevenDays => notUsedPrice * 0.2m,
                    RentalPlanType.FifteenDays => notUsedPrice * 0.4m,
                    _ => 0
                };
                return basePrice + multa;
            }
            else if (returnDate > rental.PredictedEndDate)
            {
                int extraDays = (returnDate - rental.PredictedEndDate).Days;
                decimal basePrice = daily * planDays;
                decimal extra = 50 * extraDays;
                return basePrice + extra;
            }
            else
            {
                return daily * planDays;
            }
        }

        private RentalDto ToDto(Rental entity)
        {
            return new RentalDto
            {
                Id = entity.Id,
                DeliveryManId = entity.DeliveryManId,
                MotorcycleId = entity.MotorcycleId,
                PlanType = entity.PlanType,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                PredictedEndDate = entity.PredictedEndDate,
                TotalPrice = entity.TotalPrice,
                Status = entity.Status
            };
        }
    }
}
