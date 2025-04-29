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
        private readonly IGenericRepository<TurSkabelonModel> _genericRepository;

        public TurSkabelonController(IGenericRepository<TurSkabelonModel> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        [HttpGet("read")]
        public async Task<ActionResult<List<TurSkabelonModel>>> GetAll()
        {
            var turSkabeloner = await _genericRepository.GetAllAsync();
            return Ok(turSkabeloner);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TurSkabelonModel>> GetById(int id)
        {
            var turSkabeloner = await _genericRepository.GetByIdAsync(id);
            if (turSkabeloner == null)
                return NotFound();
            return Ok(turSkabeloner);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(TurSkabelonModel turSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _genericRepository.AddAsync(turSkabelon);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(TurSkabelonModel turSkabelon)
        {
            if (ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _genericRepository.UpdateAsync(turSkabelon);
                return Ok();
            }

            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Concurrency Exception");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(TurSkabelonModel turSkabelon)
        {
            await _genericRepository.DeleteAsync(turSkabelon);
            return Ok();
        }


    }
}
