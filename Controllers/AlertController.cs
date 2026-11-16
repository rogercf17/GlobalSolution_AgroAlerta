using AgroAlerta.Data;
using AgroAlerta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroAlerta.Controllers
{
    [ApiController]
    [Route("api/alerts")]
    public class AlertController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alert>>> ListAlerts()
        {
            var alerts = await _context.Alerts
                .Include(a => a.Farm)
                .ToListAsync();

            return Ok(alerts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alert>> GetAlertById(int id)
        {
            var alert = await _context.Alerts
                .Include(f => f.Farm)
                .FirstOrDefaultAsync(alert => alert.Id == id);

            if (alert == null)
            {
                return NotFound($"Alerta de id {id} não encontrado");
            }

            return Ok(alert);
        }
    }
}
