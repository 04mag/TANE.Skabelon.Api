using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.GenericRepositories;

namespace TANE.Skabelon.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TurSkabelonController : ControllerBase
    {
        private readonly IGenericRepository<TurSkabelonModel> _turSkabelonRepository;

        public TurSkabelonController(IGenericRepository<TurSkabelonModel> genericRepository)
        {
            _turSkabelonRepository = genericRepository;
        }

        [HttpGet("read")]
        public async Task<ActionResult<List<TurSkabelonModel>>> GetAll()
        {
            var turSkabeloner = await _turSkabelonRepository.GetAllAsync();
            return Ok(turSkabeloner);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TurSkabelonModel>> GetById(int id)
        {
            var turSkabeloner = await _turSkabelonRepository.GetByIdAsync(id);
            if (turSkabeloner == null)
                return NotFound();
            return Ok(turSkabeloner);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(TurSkabelonModel turSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _turSkabelonRepository.AddAsync(turSkabelon);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(TurSkabelonModel turSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _turSkabelonRepository.UpdateAsync(turSkabelon);
                return Ok();
            }

            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Concurrency Exception");
            }
        }

        [HttpDelete("{id}")]
        public async Task DeleteTurSkabelonModelAsync(int id, byte[] originalRowVersion)
        {
            // 1) Find og tjek eksistens
            var turSkabelon = await _turSkabelonRepository.GetByIdAsync(id);
            if (turSkabelon == null)
                throw new KeyNotFoundException($"Turskabelon med id {id} ikke fundet.");

            // 2) Sæt RowVersion til det, klienten kom med
            turSkabelon.RowVersion = originalRowVersion;

            // 3) Kald repository og fang concurrency–fejl
            try
            {
                await _turSkabelonRepository.DeleteAsync(turSkabelon);
            }
            catch (Exception)
            {
                throw new(
                    $"Turskabelon med id {id} blev enten slettet eller ændret af en anden. Genindlæs og prøv igen.");
            }
        }


    }
}
