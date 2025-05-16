using TANE.Skabelon.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.GenericRepositories;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.Context;


namespace TANE.Skabelon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DagSkabelonController : ControllerBase
    {
        private readonly SkabelonDbContext skabelonDbContext;
        

        public DagSkabelonController(SkabelonDbContext skabelonDbContext)
        {
            this.skabelonDbContext = skabelonDbContext;
        }
       
        [HttpGet]
        public async Task<ActionResult<ICollection<DagSkabelonModel>>> GetAll()
        {
            return Ok(await skabelonDbContext.DagSkabelon.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DagSkabelonModel>> GetById(int id)
        {
            var result = await skabelonDbContext.DagSkabelon.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }
        //Søren review
        [HttpPost]
        public async Task<ActionResult<DagSkabelonModel>> Create(DagSkabelonModel dagSkabelonModel)
        {
                skabelonDbContext.DagSkabelon.Add(dagSkabelonModel);

                await skabelonDbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = dagSkabelonModel.Id }, dagSkabelonModel);


         }
        //Søren review
        [HttpPut("{id:int}")]
        public async Task<ActionResult<DagSkabelonModel>> Update(int id,DagSkabelonModel dagSkabelonModel)
        {
            var existingDagSkabelon = await skabelonDbContext.DagSkabelon.FindAsync(id);
            if (existingDagSkabelon == null)
            {
                return NotFound();
            }
            if (id != dagSkabelonModel.Id)
            {
                return BadRequest();
            }

            throw new NotImplementedException("Update method not implemented yet.");


        }
        //Søren review
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] byte[] rowVersion)
        {
            throw new NotImplementedException("Update method not implemented yet.");
        }


        


    }
}
