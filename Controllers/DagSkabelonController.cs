using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.GenericRepositories;
using TANE.Skabelon.Api.Models;


namespace TANE.Skabelon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DagSkabelonController : ControllerBase
    {
        private readonly IGenericRepository<DagSkabelonModel> _dagSkabelonRepository;

        public DagSkabelonController(IGenericRepository<DagSkabelonModel> genericRepository)
        {
            _dagSkabelonRepository = genericRepository;
        }

        [HttpGet("read")]
        public async Task<ActionResult<List<DagSkabelonModel>>> GetAll()
        {
            var dagSkabeloner = await _dagSkabelonRepository.GetAllAsync();
            return Ok(dagSkabeloner);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DagSkabelonModel>> GetById(int id)
        {
            var dagSkabeloner = await _dagSkabelonRepository.GetByIdAsync(id);
            if (dagSkabeloner == null)
                return NotFound();
            return Ok(dagSkabeloner);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(DagSkabelonModel dagSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _dagSkabelonRepository.AddAsync(dagSkabelon);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(DagSkabelonModel dagSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _dagSkabelonRepository.UpdateAsync(dagSkabelon);
                return Ok();
            }

            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Concurrency Exception");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task DeleteDagSkabelonModelAsync(int id, byte[] originalRowVersion)
        {
            // 1) Find og tjek eksistens
            var dagSkabelon = await _dagSkabelonRepository.GetByIdAsync(id);
            if (dagSkabelon == null)
                throw new KeyNotFoundException($"Dagskabelon med id {id} ikke fundet.");

            // 2) Sæt RowVersion til det, klienten kom med
            dagSkabelon.RowVersion = originalRowVersion;

            // 3) Kald repository og fang concurrency–fejl
            try
            {
                await _dagSkabelonRepository.DeleteAsync(dagSkabelon);
            }
            catch (Exception)
            {
                throw new(
                    $"Dagskabelon med id {id} blev enten slettet eller ændret af en anden. Genindlæs og prøv igen.");
            }
        }


    }
}
