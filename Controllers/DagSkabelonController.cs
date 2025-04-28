using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.Repositories;

namespace TANE.Skabelon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DagSkabelonController : ControllerBase
    {
        private readonly IDagSkabelonRepository _dagSkabelonRepository;

        public DagSkabelonController(IDagSkabelonRepository dagSkabelonRepository)
        {
            _dagSkabelonRepository = dagSkabelonRepository;
        }

        [HttpGet("read")]
        public async Task<ActionResult<List<DagSkabelonModel>>> GetAll()
        {
            var dagSkabeloner = await _dagSkabelonRepository.GetAllDagSkabelonerAsync();
            return Ok(dagSkabeloner);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DagSkabelonModel>> GetById(int id)
        {
            var dagSkabeloner = await _dagSkabelonRepository.GetDagSkabelonByIdAsync(id);
            if (dagSkabeloner == null)
                return NotFound();
            return Ok(dagSkabeloner);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(DagSkabelonModel dagSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _dagSkabelonRepository.AddDagSkabelonAsync(dagSkabelon);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(DagSkabelonModel dagSkabelon)
        {
            if (ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _dagSkabelonRepository.UpdateDagSkabelonAsync(dagSkabelon);
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
            await _dagSkabelonRepository.DeleteDagSkabelonAsync(id);
            return Ok();
        }


    }
}
