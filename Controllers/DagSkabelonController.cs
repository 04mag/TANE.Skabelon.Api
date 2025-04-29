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
        private readonly IGenericRepository<DagSkabelonModel> _genericRepository;

        public DagSkabelonController(IGenericRepository<DagSkabelonModel> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        [HttpGet("read")]
        public async Task<ActionResult<List<DagSkabelonModel>>> GetAll()
        {
            var dagSkabeloner = await _genericRepository.GetAllAsync();
            return Ok(dagSkabeloner);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DagSkabelonModel>> GetById(int id)
        {
            var dagSkabeloner = await _genericRepository.GetByIdAsync(id);
            if (dagSkabeloner == null)
                return NotFound();
            return Ok(dagSkabeloner);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(DagSkabelonModel dagSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _genericRepository.AddAsync(dagSkabelon);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(DagSkabelonModel dagSkabelon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _genericRepository.UpdateAsync(dagSkabelon);
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
        public async Task<ActionResult> Delete(int id)
        {
            await _genericRepository.DeleteAsync(id);
            return Ok();
        }


    }
}
