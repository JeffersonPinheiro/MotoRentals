using Microsoft.AspNetCore.Mvc;
using MotorcycleRentals.src.Application.DTOs;
using MotorcycleRentals.src.Application.Interfaces;
using MotorcycleRentals.src.Application.Services;
using MotorcycleRentals.src.Domain.Entities;

namespace MotorcycleRentals.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcycleController : ControllerBase
    {
        private readonly IMotorcycleService _motorcycleService;

        public MotorcycleController(IMotorcycleService motorcycleService)
        {
            _motorcycleService = motorcycleService;
        }

        /// <summary>
        /// Cadastra uma nova motocicleta.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MotorcycleCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _motorcycleService.CreateMotorcycleAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Consulta motocicletas, podendo filtrar por placa.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? plate)
        {
            var motos = await _motorcycleService.GetMotorcyclesAsync(plate);
            return Ok(motos);
        }

        /// <summary>
        /// Consulta motocicleta por Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var moto = await _motorcycleService.GetByIdAsync(id);
            if (moto == null)
                return NotFound();
            return Ok(moto);
        }

        /// <summary>
        /// Atualiza a placa da motocicleta.
        /// </summary>
        [HttpPut("{id}/plate")]
        public async Task<IActionResult> UpdatePlate(Guid id, [FromBody] MotorcycleUpdatePlateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _motorcycleService.UpdatePlateAsync(id, dto);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma motocicleta (se não estiver alugada).
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _motorcycleService.DeleteMotorcycleAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
