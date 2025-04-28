using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.Repositories;

namespace TANE.Skabelon.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TurSkabelonController : ControllerBase
    {
        private readonly ITurSkabelonRepository _turSkabelonRepository;

        public TurSkabelonController(ITurSkabelonRepository turSkabelonRepository)
        {
            _turSkabelonRepository = turSkabelonRepository;
        }

        [HttpGet("read")]
        public async Task<ActionResult<List<TurSkabelonModel>>> GetAll()
        {
            var turSkabeloner = await _turSkabelonRepository.GetAllTurSkabelonerAsync();
            return Ok(turSkabeloner);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TurSkabelonModel>> GetById(int id)
        {
            var turSkabeloner = await _turSkabelonRepository.GetTurSkabelonByIdAsync(id);
            if (turSkabeloner == null)
                return NotFound();
            return Ok(turSkabeloner);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(TurSkabelonModel turSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _turSkabelonRepository.AddTurSkabelonAsync(turSkabelon);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(TurSkabelonModel turSkabelon)
        {
            if (ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _turSkabelonRepository.UpdateTurSkabelonAsync(turSkabelon);
                return Ok();
            }

            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Concurrency Exception");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _turSkabelonRepository.DeleteTurSkabelonAsync(id);
            return Ok();
        }


    }
}
