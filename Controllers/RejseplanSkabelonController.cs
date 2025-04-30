using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.GenericRepositories;

namespace TANE.Skabelon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejseplanSkabelonController : ControllerBase
    {
        private readonly IGenericRepository<RejseplanSkabelonModel> _rejseplanSkabelonRepository;

        public RejseplanSkabelonController(IGenericRepository<RejseplanSkabelonModel> genericRepository)
        {
            _rejseplanSkabelonRepository = genericRepository;
        }

        [HttpGet("read")]
        public async Task<ActionResult<List<RejseplanSkabelonModel>>> GetAll()
        {
            var rejseplanSkabelon = await _rejseplanSkabelonRepository.GetAllAsync();
            return Ok(rejseplanSkabelon);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RejseplanSkabelonModel>> GetById(int id)
        {
            var rejseplanSkabelon = await _rejseplanSkabelonRepository.GetByIdAsync(id);
            if (rejseplanSkabelon == null)
                return NotFound();
            return Ok(rejseplanSkabelon);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(RejseplanSkabelonModel rejseplanSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _rejseplanSkabelonRepository.AddAsync(rejseplanSkabelon);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(RejseplanSkabelonModel rejseplanSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _rejseplanSkabelonRepository.UpdateAsync(rejseplanSkabelon);
                return Ok();
            }

            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Concurrency Exception");
            }
        }

        [HttpDelete("{id}")]
        public async Task DeleteRejseplanSkabelonModelAsync(int id, byte[] originalRowVersion)
        {
            // 1) Find og tjek eksistens
            var rejseplanSkabelon = await _rejseplanSkabelonRepository.GetByIdAsync(id);
            if (rejseplanSkabelon == null)
                throw new KeyNotFoundException($"Rejseplanskabelon med id {id} ikke fundet.");

            // 2) Sæt RowVersion til det, klienten kom med
            rejseplanSkabelon.RowVersion = originalRowVersion;

            // 3) Kald repository og fang concurrency–fejl
            try
            {
                await _rejseplanSkabelonRepository.DeleteAsync(rejseplanSkabelon);
            }
            catch (Exception)
            {
                throw new(
                    $"Rejseplanskabelon med id {id} blev enten slettet eller ændret af en anden. Genindlæs og prøv igen.");
            }
        }
    }
 }
    
