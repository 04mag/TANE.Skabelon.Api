using AutoMapper;
using TANE.Skabelon.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.GenericRepositories;
using Microsoft.IdentityModel.Tokens;
using TANE.Skabelon.Api.Context;
using Microsoft.AspNetCore.Http.Features;

namespace TANE.Skabelon.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TurSkabelonController : ControllerBase
    {
        private readonly DbContextOptions<SkabelonDbContext> options;

        public TurSkabelonController(DbContextOptions<SkabelonDbContext> options)
        {
            this.options = options;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TurSkabelonModel>>> GetAll()
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                try
                {
                    return Ok(await skabelonDbContext.TurSkabelon.Include(x => x.DagTurSkabelon).ThenInclude(x => x.DagSkabelon).ToListAsync());
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ICollection<TurSkabelonModel>>> GetById(int id)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                try
                {
                    var result = await skabelonDbContext.TurSkabelon.Include(x => x.DagTurSkabelon).ThenInclude(x => x.DagSkabelon).FirstOrDefaultAsync(x => x.Id == id);

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
        public async Task<ActionResult<TurSkabelonModel>> Create(TurSkabelonModel turSkabelonModel)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                try
                {
                    skabelonDbContext.TurSkabelon.Add(turSkabelonModel);

                    await skabelonDbContext.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetById), new { id = turSkabelonModel.Id }, turSkabelonModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TurSkabelonModel>> Update(int id, TurSkabelonModel turSkabelonModel)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                using (var contextTransaction = skabelonDbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    
                    try
                    {
                        var existingTurSkabelon = await skabelonDbContext.TurSkabelon.Include(t => t.DagTurSkabelon).ThenInclude(t => t.DagSkabelon).FirstOrDefaultAsync(t => t.Id == id);

                        if (existingTurSkabelon == null)
                        {
                            return NotFound();
                        }

                        if (id != turSkabelonModel.Id)
                        {
                            return BadRequest();
                        }

                        //Update the existing TurSkabelon properties
                        existingTurSkabelon.Titel = turSkabelonModel.Titel;
                        existingTurSkabelon.Beskrivelse = turSkabelonModel.Beskrivelse;
                        existingTurSkabelon.Pris = turSkabelonModel.Pris;

                        // Update order for each DagTurSkabelon  
                        foreach (var dagTur in turSkabelonModel.DagTurSkabelon)
                        {
                            var existingDagTur = existingTurSkabelon.DagTurSkabelon.FirstOrDefault(d => d.DagSkabelonId == dagTur.DagSkabelonId);
                            if (existingDagTur != null)
                            {
                                existingDagTur.Order = dagTur.Order;
                            }
                            else
                            {
                                existingTurSkabelon.DagTurSkabelon.Add(new DagTurSkabelon
                                {
                                    DagSkabelonId = dagTur.DagSkabelonId,
                                    Order = dagTur.Order
                                });
                            }
                        }

                        // Remove any DagTurSkabelon that are no longer in the updated model  
                        existingTurSkabelon.DagTurSkabelon.RemoveAll(d => !turSkabelonModel.DagTurSkabelon.Any(updated => updated.DagSkabelonId == d.DagSkabelonId));

                        await skabelonDbContext.SaveChangesAsync();

                        return Ok(existingTurSkabelon);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return Conflict("Concurrency conflict occurred while updating the TurSkabelon.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                try
                {
                    var turSkabelon = await skabelonDbContext.TurSkabelon.FindAsync(id);
                    if (turSkabelon == null)
                    {
                        return NotFound();
                    }
                    skabelonDbContext.TurSkabelon.Remove(turSkabelon);
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
