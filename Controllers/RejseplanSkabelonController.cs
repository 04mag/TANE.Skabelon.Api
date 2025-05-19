
using AutoMapper;
using TANE.Skabelon.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.GenericRepositories;
using Microsoft.Extensions.Options;
using TANE.Skabelon.Api.Context;

namespace TANE.Skabelon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejseplanSkabelonController : ControllerBase
    {
        private readonly DbContextOptions<SkabelonDbContext> options;

        public RejseplanSkabelonController(DbContextOptions<SkabelonDbContext> options)
        {
            this.options = options;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<RejseplanSkabelonModel>>> GetAll()
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                try
                {
                    return Ok(await skabelonDbContext.RejseplanSkabelon.Include(x => x.RejseplanTurSkabelon).ThenInclude(x => x.TurSkabelon).ToListAsync());
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RejseplanSkabelonModel>> GetById(int id)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                try
                {
                    var result = await skabelonDbContext.RejseplanSkabelon.Include(x => x.RejseplanTurSkabelon).ThenInclude(x => x.TurSkabelon).FirstOrDefaultAsync(x => x.Id == id);

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
        public async Task<ActionResult<RejseplanSkabelonModel>> Create(RejseplanSkabelonModel rejseplanSkabelonModel)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                using (var contextTransaction = skabelonDbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        foreach (var rejseTur in rejseplanSkabelonModel.RejseplanTurSkabelon)
                        {
                            if (rejseTur.TurSkabelon != null && rejseTur.TurSkabelon.Id > 0)
                            {
                                var dagSkabelon = rejseTur.TurSkabelon;
                                rejseTur.TurSkabelon = null;
                            }
                            else if (rejseTur.TurSkabelon != null && rejseTur.TurSkabelon.Id == 0)
                            {
                                throw new Exception("TurSkabelon must already exist");
                            }

                            if (rejseTur.RejseplanSkabelon != null)
                            {
                                throw new Exception("RejseplanSkabelon must be null");
                            }
                        }

                        skabelonDbContext.RejseplanSkabelon.Add(rejseplanSkabelonModel);

                        await skabelonDbContext.SaveChangesAsync();

                        var createdRejseSkabelon = await skabelonDbContext.RejseplanSkabelon
                                .Include(x => x.RejseplanTurSkabelon)
                                    .ThenInclude(dt => dt.TurSkabelon)
                                .FirstOrDefaultAsync(x => x.Id == rejseplanSkabelonModel.Id);

                        contextTransaction.Commit();

                        return CreatedAtAction(nameof(GetById), new { id = rejseplanSkabelonModel.Id }, createdRejseSkabelon);
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
        public async Task<ActionResult<RejseplanSkabelonModel>> Update(int id, RejseplanSkabelonModel rejseplanSkabelonModel)
        {
            using (var skabelonDbContext = new SkabelonDbContext(options))
            {
                using (var contextTransaction = skabelonDbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {

                    try
                    {
                        var existingRejseplanSkabelon = await skabelonDbContext.RejseplanSkabelon.Include(t => t.RejseplanTurSkabelon).ThenInclude(t => t.TurSkabelon).FirstOrDefaultAsync(t => t.Id == id);

                        if (existingRejseplanSkabelon == null)
                        {
                            return NotFound();
                        }

                        if (id != rejseplanSkabelonModel.Id)
                        {
                            return BadRequest();
                        }

                        //Update the existing TurSkabelon properties
                        existingRejseplanSkabelon.Titel = rejseplanSkabelonModel.Titel;
                        existingRejseplanSkabelon.Beskrivelse = rejseplanSkabelonModel.Beskrivelse;

                        //For concurrency check
                        skabelonDbContext.Entry(existingRejseplanSkabelon).Property(p => p.RowVersion).OriginalValue = rejseplanSkabelonModel.RowVersion;

                        // Update order for each DagTurSkabelon  
                        foreach (var rejseplanTur in rejseplanSkabelonModel.RejseplanTurSkabelon)
                        {
                            var existingRejseplanTur = existingRejseplanSkabelon.RejseplanTurSkabelon.FirstOrDefault(d => d.TurSkabelonId == rejseplanTur.TurSkabelonId);
                            if (existingRejseplanTur != null)
                            {
                                existingRejseplanTur.Order = rejseplanTur.Order;
                                //For concurrency check
                                skabelonDbContext.Entry(existingRejseplanTur).Property(p => p.RowVersion).OriginalValue = rejseplanTur.RowVersion;
                            }
                            else
                            {
                                existingRejseplanSkabelon.RejseplanTurSkabelon.Add(new RejseplanTurSkabelon
                                {
                                    TurSkabelonId = rejseplanTur.TurSkabelonId,
                                    Order = rejseplanTur.Order
                                });
                            }
                        }

                        // Remove any DagTurSkabelon that are no longer in the updated model  
                        existingRejseplanSkabelon.RejseplanTurSkabelon.RemoveAll(d => !rejseplanSkabelonModel.RejseplanTurSkabelon.Any(updated => updated.TurSkabelonId == d.TurSkabelonId));

                        // Save changes to the database
                        await skabelonDbContext.SaveChangesAsync();

                        var updatedEntity = skabelonDbContext.RejseplanSkabelon
                            .Include(t => t.RejseplanTurSkabelon)
                            .ThenInclude(t => t.TurSkabelon)
                            .AsNoTracking()
                            .FirstOrDefault();

                        // Commit the transaction
                        await contextTransaction.CommitAsync();

                        return Ok(existingRejseplanSkabelon);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return Conflict("Concurrency conflict occurred while updating rejseplan skabelon.");
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
                    var rejseSkabelon = await skabelonDbContext.RejseplanSkabelon.FindAsync(id);
                    if (rejseSkabelon == null)
                    {
                        return NotFound();
                    }
                    skabelonDbContext.RejseplanSkabelon.Remove(rejseSkabelon);
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
    
