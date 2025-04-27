using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
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
                var rejseplanSkabeloner = await _rejseplanSkabelonRepository.GetAllRejseplanSkabelonerAsync();
                return Ok(rejseplanSkabeloner);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<RejseplanSkabelonModel>> GetById(int id)
            {
                var rejseplanSkabeloner = await _rejseplanSkabelonRepository.GetRejseplanSkabelonerByIdAsync(id);
                if (rejseplanSkabeloner == null)
                    return NotFound();
                return Ok(rejseplanSkabeloner);
            }

            [HttpPost("create")]
            public async Task<ActionResult> Create(RejseplanSkabelonModel rejseplanSkabelon)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _rejseplanSkabelonRepository.AddRejseplanSkabelonerAsync(rejseplanSkabelon);
                return Ok();
            }

            [HttpPut("update")]
            public async Task<ActionResult> Update(RejseplanSkabelonModel rejseplanSkabelon)
            {
                if (ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    await _rejseplanSkabelonRepository.UpdateRejseplanSkabelonerAsync(rejseplanSkabelon);
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
