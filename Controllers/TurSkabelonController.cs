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
        public async Task<ActionResult<TurSkabelonModel>> GetById(int id)
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
                using (var contextTransaction = skabelonDbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        foreach (var dagTur in turSkabelonModel.DagTurSkabelon)
                        {
                            if (dagTur.DagSkabelon != null && dagTur.DagSkabelon.Id > 0)
                            {
                                var dagSkabelon = dagTur.DagSkabelon;
                                dagTur.DagSkabelon = null;
                            }
                            else if (dagTur.DagSkabelon != null && dagTur.DagSkabelon.Id == 0)
                            {
                                throw new Exception("DagSkabelon must already exist");
                            }

                            if (dagTur.TurSkabelon != null)
                            {
                                throw new Exception("TurSkabelon must be null");
                            }
                        }

                        skabelonDbContext.TurSkabelon.Add(turSkabelonModel);

                        await skabelonDbContext.SaveChangesAsync();

                        var createdTurSkabelon = await skabelonDbContext.TurSkabelon
                            .Include(x => x.DagTurSkabelon)
                                .ThenInclude(dt => dt.DagSkabelon)
                            .FirstOrDefaultAsync(x => x.Id == turSkabelonModel.Id);

                        await contextTransaction.CommitAsync();

                        return CreatedAtAction(nameof(GetById), new { id = turSkabelonModel.Id }, createdTurSkabelon);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
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

                        //Update the existing DagSkabelon properties
                        existingTurSkabelon.Titel = turSkabelonModel.Titel;
                        existingTurSkabelon.Beskrivelse = turSkabelonModel.Beskrivelse;
                        existingTurSkabelon.Pris = turSkabelonModel.Pris;

                        //For concurrency check
                        skabelonDbContext.Entry(existingTurSkabelon).Property(p => p.RowVersion).OriginalValue = turSkabelonModel.RowVersion;


                        // Update order for each DagTurSkabelon  
                        foreach (var dagTur in turSkabelonModel.DagTurSkabelon)
                        {
                            var existingDagTur = existingTurSkabelon.DagTurSkabelon.FirstOrDefault(d => d.DagSkabelonId == dagTur.DagSkabelonId);
                            if (existingDagTur != null)
                            {
                                existingDagTur.Order = dagTur.Order;
                                //For concurrency check
                                skabelonDbContext.Entry(existingDagTur).Property(p => p.RowVersion).OriginalValue = dagTur.RowVersion;
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

                        // Save changes to the database
                        await skabelonDbContext.SaveChangesAsync();

                        var updatedEntity = skabelonDbContext.TurSkabelon
                            .Include(t => t.DagTurSkabelon)
                            .ThenInclude(t => t.DagSkabelon)
                            .AsNoTracking()
                            .FirstOrDefault();

                        // Commit the transaction
                        await contextTransaction.CommitAsync();

                        return Ok(updatedEntity);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return Conflict("Concurrency conflict occurred while updating tur skabelon.");
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
