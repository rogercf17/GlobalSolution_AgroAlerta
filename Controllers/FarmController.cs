using AgroAlerta.Data;
using AgroAlerta.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace AgroAlerta.Controllers
{
    [ApiController]
    [Route("api/farms")]
    public class FarmController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FarmController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Farm>>> ListAllFarms()
        {
            var farms = await _context.Farms
                .Include(r => r.Readings)
                .Include(a => a.Alerts)
                .ToListAsync();
            return Ok(farms);
        }

        [HttpPost]
        public async Task<ActionResult<Farm>> CreateFarm(Farm newFarm)
        {
            _context.Farms.Add(newFarm);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFarmById), new { id = newFarm.Id }, newFarm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Farm>> GetFarmById(int id)
        {
            var farm = await _context.Farms
                .Include(f => f.Readings)
                .Include(f => f.Alerts)
                .FirstOrDefaultAsync(reading => reading.Id == id);

            if (farm == null)
            {
                return NotFound($"Fazenda de id {id} não encontrado");
            }

            return Ok(farm);
        }
    }
}
