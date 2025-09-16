using Microsoft.AspNetCore.Mvc;
using MotorcycleRentals.src.Infrastructure.Persistence.Repositories;

namespace MotorcycleRentals.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcycleNotificationController : ControllerBase
    {
        private readonly IMotorcycleNotificationRepository _notificationRepo;

        public MotorcycleNotificationController(IMotorcycleNotificationRepository notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        /// <summary>
        /// Retorna todas as notificações de motos persistidas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationRepo.GetNotificationsAsync();
            return Ok(notifications);
        }

        /// <summary>
        /// Retorna notificações de motos filtradas por ano.
        /// </summary>
        [HttpGet("year/{year}")]
        public async Task<IActionResult> GetByYear(int year)
        {
            var notifications = await _notificationRepo.GetNotificationsByYearAsync(year);
            return Ok(notifications);
        }

        /// <summary>
        /// Retorna notificações filtradas por placa (parcial ou completa).
        /// </summary>
        [HttpGet("plate/{plate}")]
        public async Task<IActionResult> GetByPlate(string plate)
        {
            var notifications = await _notificationRepo.GetNotificationsByPlateAsync(plate);
            return Ok(notifications);
        }
    }
}
