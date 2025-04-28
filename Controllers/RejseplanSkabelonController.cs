using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.Repositories;
using TANE.Skabelon.Api.Repository;

namespace TANE.Skabelon.Api.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class RejseplanSkabelonController : ControllerBase
        {
            private readonly IRejseplanSkabelonRepository _rejseplanSkabelonRepository;

            public RejseplanSkabelonController(IRejseplanSkabelonRepository rejseplanSkabelonRepository)
            {
                _rejseplanSkabelonRepository = rejseplanSkabelonRepository;
            }

            [HttpGet("read")]
            public async Task<ActionResult<List<RejseplanSkabelonModel>>> GetAll()
            {
                var rejseplanSkabelon = await _rejseplanSkabelonRepository.GetAllRejseplanSkabelonerAsync();
                return Ok(rejseplanSkabelon);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<RejseplanSkabelonModel>> GetById(int id)
            {
                var rejseplanSkabelon = await _rejseplanSkabelonRepository.GetRejseplanSkabelonByIdAsync(id);
                if (rejseplanSkabelon == null)
                    return NotFound();
                return Ok(rejseplanSkabelon);
            }

            [HttpPost("create")]
            public async Task<ActionResult> Create(RejseplanSkabelonModel rejseplanSkabelon)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _rejseplanSkabelonRepository.AddRejseplanSkabelonAsync(rejseplanSkabelon);
                return Ok();
            }

            [HttpPut("update")]
            public async Task<ActionResult> Update(RejseplanSkabelonModel rejseplanSkabelon)
            {
                if (ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    await _rejseplanSkabelonRepository.UpdateRejseplanSkabelonAsync(rejseplanSkabelon);
                    return Ok();
                }

                catch (DbUpdateConcurrencyException) 
                {
                    return Conflict("Concurrency Exception");
                }
            }

            [HttpDelete("{id}")]
            public async Task <ActionResult> Delete(int id)
            {
                await _rejseplanSkabelonRepository.DeleteRejseplanSkabelonAsync(id);
                return Ok();
            }


        }
    }
