using Microsoft.AspNetCore.Mvc;
using MotorcycleRentals.src.Application.DTOs;
using MotorcycleRentals.src.Application.Interfaces;
using MotorcycleRentals.src.Application.Services;
using MotorcycleRentals.src.Domain.Entities;

namespace MotorcycleRentals.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        /// <summary>
        /// Cria uma nova locação de motocicleta.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RentMotorcycle([FromBody] RentalCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _rentalService.CreateRentalAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Consulta uma locação pelo Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rental = await _rentalService.GetByIdAsync(id);
            if (rental == null)
                return NotFound();
            return Ok(rental);
        }

        /// <summary>
        /// Finaliza uma locação informando a data de devolução.
        /// </summary>
        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnMotorcycle(Guid id, [FromBody] RentalReturnDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Garante que o id do caminho seja usado no DTO
            dto.RentalId = id;

            var result = await _rentalService.ReturnRentalAsync(dto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
