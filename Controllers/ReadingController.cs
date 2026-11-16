using AgroAlerta.Data;
using AgroAlerta.Interfaces;
using AgroAlerta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroAlerta.Controllers
{
    [ApiController]
    [Route("api/readings")]
    public class ReadingController : ControllerBase 
    {
        private readonly AppDbContext _context;
        private readonly IAlertService _service;

        public ReadingController(AppDbContext context, IAlertService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorReading>>> ListAllReadings()
        {
            var readings = await _context.SensorReadings
                .Include(r => r.Farm)
                .ToListAsync();
            return Ok(readings);
        }

        [HttpPost]
        public async Task<ActionResult<SensorReading>> RegisterReading(SensorReading newReading)
        {
            var farmExiste = await _context.Farms.AnyAsync(f => f.Id == newReading.FarmId);
            if (!farmExiste)
            {
                return BadRequest($"A Farm com Id {newReading.FarmId} não foi encontrada.");
            }

            _context.SensorReadings.Add(newReading);

            var alertsGerados = _service.AnalyzeReading(newReading);

            if (alertsGerados.Any())
            {
                _context.Alerts.AddRange(alertsGerados);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReadingById), new { id = newReading.Id }, newReading);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorReading>> GetReadingById(int id)
        {
            var reading = await _context.SensorReadings
                .Include(f => f.Farm)
                .FirstOrDefaultAsync(reading => reading.Id == id);

            if (reading == null)
            {
                return NotFound($"Leitura de id {id} não encontrado");
            }

            return Ok(reading);
        }
    }
}
