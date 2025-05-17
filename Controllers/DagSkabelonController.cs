using TANE.Skabelon.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.GenericRepositories;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.Context;
using Microsoft.Extensions.Options;


namespace TANE.Skabelon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DagSkabelonController : ControllerBase
    {
        private readonly DbContextOptions<SkabelonDbContext> options;

        public DagSkabelonController(DbContextOptions<SkabelonDbContext> options)
        {
            this.options = options;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<DagSkabelonModel>>> GetAll()
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                try
                {
                    return Ok(await skabelonDbContext.DagSkabelon.ToListAsync());
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DagSkabelonModel>> GetById(int id)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                try
                {
                    var result = await skabelonDbContext.DagSkabelon.FirstOrDefaultAsync(x => x.Id == id);

                    if (result == null)
                    {
                        return NotFound();
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }

        }
        
        [HttpPost]
        public async Task<ActionResult<DagSkabelonModel>> Create(DagSkabelonModel dagSkabelonModel)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                try
                {
                    skabelonDbContext.DagSkabelon.Add(dagSkabelonModel);

                    await skabelonDbContext.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetById), new { id = dagSkabelonModel.Id }, dagSkabelonModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }

        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<DagSkabelonModel>> Update(int id,DagSkabelonModel dagSkabelonModel)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                using (var contextTransaction = skabelonDbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        skabelonDbContext.Update(dagSkabelonModel);

                        await skabelonDbContext.SaveChangesAsync();

                        var updatedEntity = await skabelonDbContext.DagSkabelon.FirstOrDefaultAsync(x => x.Id == id);

                        contextTransaction.Commit();

                        return Ok(updatedEntity);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return Conflict("Concurrency conflict occurred while updating the DagSkabelon.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                try
                {
                    var dagSkabelon = await skabelonDbContext.DagSkabelon.FindAsync(id);
                    if (dagSkabelon == null)
                    {
                        return NotFound();
                    }
                    skabelonDbContext.Remove(dagSkabelon);
                    await skabelonDbContext.SaveChangesAsync();

                    return NoContent();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }


        


    }
}
