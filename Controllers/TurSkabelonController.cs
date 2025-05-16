using AutoMapper;
using TANE.Skabelon.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.GenericRepositories;
using Microsoft.IdentityModel.Tokens;
using TANE.Skabelon.Api.Context;

namespace TANE.Skabelon.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TurSkabelonController : ControllerBase
    {
        private readonly SkabelonDbContext skabelonDbContext;

        public TurSkabelonController(SkabelonDbContext skabelonDbContext)
        {
            this.skabelonDbContext = skabelonDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TurSkabelonModel>>> GetAll()
        {
            return Ok(await skabelonDbContext.TurSkabelon.Include(x => x.DagTurSkabelon).ThenInclude(x => x.DagSkabelon).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ICollection<TurSkabelonModel>>> GetById(int id)
        {
            var result = await skabelonDbContext.TurSkabelon.Include(x => x.DagTurSkabelon).ThenInclude(x => x.DagSkabelon).FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TurSkabelonModel>> Create(TurSkabelonModel turSkabelonModel)
        {
            skabelonDbContext.TurSkabelon.Add(turSkabelonModel);

            await skabelonDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = turSkabelonModel.Id }, turSkabelonModel);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TurSkabelonModel>> Update(int id, TurSkabelonModel turSkabelonModel)
        {
            var existingTurSkabelon = await skabelonDbContext.TurSkabelon.FindAsync(id);

            if (existingTurSkabelon == null)
            {
                return NotFound();
            }

            if (id != turSkabelonModel.Id)
            {
                return BadRequest();
            }

            existingTurSkabelon.Titel = turSkabelonModel.Titel;
            existingTurSkabelon.Beskrivelse = turSkabelonModel.Beskrivelse;
            existingTurSkabelon.Pris = turSkabelonModel.Pris;
            existingTurSkabelon.DagTurSkabelon = turSkabelonModel.DagTurSkabelon;
            existingTurSkabelon.RejseplanTurSkabelon = turSkabelonModel.RejseplanTurSkabelon;
            skabelonDbContext.Entry(existingTurSkabelon).State = EntityState.Modified;
            
            await skabelonDbContext.SaveChangesAsync();

            return Ok(existingTurSkabelon);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
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


    }
}
