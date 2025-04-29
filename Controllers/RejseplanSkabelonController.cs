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
            private readonly IGenericRepository<RejseplanSkabelonModel> _genericRepository;

            public RejseplanSkabelonController(IGenericRepository<RejseplanSkabelonModel> genericRepository)
            {
                _genericRepository = genericRepository;
            }

            [HttpGet("read")]
            public async Task<ActionResult<List<RejseplanSkabelonModel>>> GetAll()
            {
                var rejseplanSkabelon = await _genericRepository.GetAllAsync();
                return Ok(rejseplanSkabelon);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<RejseplanSkabelonModel>> GetById(int id)
            {
                var rejseplanSkabelon = await _genericRepository.GetByIdAsync(id);
                if (rejseplanSkabelon == null)
                    return NotFound();
                return Ok(rejseplanSkabelon);
            }

            [HttpPost("create")]
            public async Task<ActionResult> Create(RejseplanSkabelonModel rejseplanSkabelon)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _genericRepository.AddAsync(rejseplanSkabelon);
                return Ok();
            }

            [HttpPut("update")]
            public async Task<ActionResult> Update(RejseplanSkabelonModel rejseplanSkabelon)
            {
                if (ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    await _genericRepository.UpdateAsync(rejseplanSkabelon);
                    return Ok();
                }

                catch (DbUpdateConcurrencyException) 
                {
                    return Conflict("Concurrency Exception");
                }
            }

            [HttpDelete("{id}")]
            public async Task <ActionResult> Delete(RejseplanSkabelonModel rejseplanSkabelon)
            {
                await _genericRepository.DeleteAsync(rejseplanSkabelon);
                return Ok();
            }


        }
    }
