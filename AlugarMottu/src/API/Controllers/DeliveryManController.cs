using Microsoft.AspNetCore.Mvc;
using MotorcycleRentals.src.Application.DTOs;
using MotorcycleRentals.src.Application.Interfaces;
using MotorcycleRentals.src.Application.Services;
using MotorcycleRentals.src.Domain.Entities;

namespace MotorcycleRentals.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryManController : ControllerBase
    {
        private readonly IDeliveryManService _deliveryManService;

        public DeliveryManController(IDeliveryManService deliveryManService)
        {
            _deliveryManService = deliveryManService;
        }

        /// <summary>
        /// Cadastra um novo entregador.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] DeliveryManCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _deliveryManService.RegisterDeliveryManAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Consulta entregador por Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var deliveryMan = await _deliveryManService.GetByIdAsync(id);
            if (deliveryMan == null)
                return NotFound();
            return Ok(deliveryMan);
        }

        /// <summary>
        /// Realiza upload da imagem da CNH do entregador.
        /// A imagem é salva no disco, não no banco.
        /// </summary>
        [HttpPost("{id}/cnh-image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadCnhImage(Guid id, [FromForm] UploadCnhRequest request)
        {
            var allowedTypes = new[] { "image/png", "image/bmp" };
            if (request.CnhFile == null || !allowedTypes.Contains(request.CnhFile.ContentType))
                return BadRequest("Invalid file format. Only PNG and BMP allowed.");

            var dto = new DeliveryManCnhImageDto
            {
                ImageFile = request.CnhFile
            };

            try
            {
                var updated = await _deliveryManService.UpdateCnhImageAsync(id, dto);
                if (updated == null)
                    return NotFound();
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
