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
                    DagSkabelonModel? existingDagSkabelon = null;
                    try
                    {
                        existingDagSkabelon = await skabelonDbContext.DagSkabelon.FirstOrDefaultAsync(t => t.Id == id);

                        if (existingDagSkabelon == null)
                        {
                            return NotFound();
                        }

                        if (id != dagSkabelonModel.Id)
                        {
                            return BadRequest();
                        }

                        //Update the existing DagSkabelon properties
                        existingDagSkabelon.Titel = dagSkabelonModel.Titel;
                        existingDagSkabelon.Beskrivelse = dagSkabelonModel.Beskrivelse;
                        

                        // Update order for each DagTurSkabelon  
                        foreach (var dagTur in dagSkabelonModel.DagTurSkabelon)
                        {
                            var existingDagTur = existingDagSkabelon.DagTurSkabelon.FirstOrDefault(d => d.DagSkabelonId == dagTur.DagSkabelonId);
                            if (existingDagTur != null)
                            {
                                existingDagTur.Order = dagTur.Order;
                            }
                            else
                            {
                                existingDagSkabelon.DagTurSkabelon.Add(new DagTurSkabelon
                                {
                                    DagSkabelonId = dagTur.DagSkabelonId,
                                    Order = dagTur.Order
                                });
                            }
                        }

                        // Remove any DagTurSkabelon that are no longer in the updated model  
                        existingDagSkabelon.DagTurSkabelon.RemoveAll(d => !dagSkabelonModel.DagTurSkabelon.Any(updated => updated.DagSkabelonId == d.DagSkabelonId));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }

                    try
                    {
                        await skabelonDbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return Conflict("Concurrency conflict occurred while updating the DagSkabelon.");
                    }

                    return Ok(existingDagSkabelon);
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
