using AutoMapper;
using TANE.Skabelon.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.GenericRepositories;
using Microsoft.IdentityModel.Tokens;

namespace TANE.Skabelon.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TurSkabelonController : ControllerBase
    {
        private readonly IGenericRepository<TurSkabelonModel> _turSkabelonRepository;
        private readonly IMapper _mapper;

        public TurSkabelonController(IGenericRepository<TurSkabelonModel> genericRepository, IMapper mapper)
        {
            _turSkabelonRepository = genericRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurSkabelonReadDto>>> GetAll()
        {
            var turSkabelon = await _turSkabelonRepository.GetAllAsync(q => q.Include(t => t.Dage));
            return Ok(_mapper.Map<IEnumerable<TurSkabelonReadDto>>(turSkabelon));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<TurSkabelonReadDto>>> GetById(int id)
        {
            var turSkabelon = await _turSkabelonRepository.GetByIdWithIncludeAsync(id, d=> d.Dage);
            if (turSkabelon == null)
                return NotFound();

            return Ok(_mapper.Map<TurSkabelonReadDto>(turSkabelon));
        }

        [HttpPost]
        public async Task<ActionResult<TurSkabelonReadDto>> Create([FromBody] TurSkabelonCreateDto dto)
        {
            var turSkabelon = _mapper.Map<TurSkabelonModel>(dto);
            await _turSkabelonRepository.AddAsync(turSkabelon);
            var readDto = _mapper.Map<TurSkabelonReadDto>(turSkabelon);
            return CreatedAtAction(nameof(GetById), new {id = readDto.Id}, readDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TurSkabelonUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("URL-id og DTO-id skal være ens.");

            // 2a) Hent aggregate inkl. alle dage
            var turSkabelon = await _turSkabelonRepository
                .GetByIdWithIncludeAsync(id, ts => ts.Dage!);
            if (turSkabelon == null)
                return NotFound($"TurSkabelon med id={id} ikke fundet.");

            // 2b) Opdater parent-felter
            turSkabelon.Titel = dto.Titel;
            turSkabelon.Beskrivelse = dto.Beskrivelse;
            turSkabelon.Pris = dto.Pris;
            turSkabelon.Sekvens = dto.Sekvens;

            // 3) Diff nested Dage
            var incomingIds = dto.Dage
                                 .Where(d => d.Id > 0)
                                 .Select(d => d.Id)
                                 .ToHashSet();

            // 3a) Slet de dage, som DTO’en ikke længere refererer til
            foreach (var dag in turSkabelon.Dage!
                               .Where(d => !incomingIds.Contains(d.Id))
                               .ToList())
            {
                turSkabelon.Dage!.Remove(dag);
            }

            // 3b) Opdater eksisterende dage (Id > 0)
            foreach (var dagDto in dto.Dage.Where(d => d.Id > 0))
            {
                var dagEntity = turSkabelon.Dage!
                    .Single(d => d.Id == dagDto.Id);

                dagEntity.Titel = dagDto.Titel;
                dagEntity.Beskrivelse = dagDto.Beskrivelse;
                dagEntity.Aktiviteter = dagDto.Aktiviteter;
                dagEntity.Måltider = dagDto.Måltider;
                dagEntity.Overnatning = dagDto.Overnatning;
                dagEntity.Sekvens = dagDto.Sekvens;
                dagEntity.RowVersion = dagDto.RowVersion;
            }

            // 3c) Tilføj nye dage (Id == 0)
            foreach (var dagDto in dto.Dage.Where(d => d.Id == 0))
            {
                turSkabelon.Dage!.Add(new DagSkabelonModel
                {
                    Titel = dagDto.Titel,
                    Beskrivelse = dagDto.Beskrivelse,
                    Aktiviteter = dagDto.Aktiviteter,
                    Måltider = dagDto.Måltider,
                    Overnatning = dagDto.Overnatning,
                    Sekvens = dagDto.Sekvens,
                });
            }

            // 4) Persistér alt i én transaktion
            try
            {
                // Marker aggregate som opdateret
                _turSkabelonRepository.Update(turSkabelon);
                await _turSkabelonRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Data er blevet ændret af en anden bruger. Hent venligst det nyeste.");
            }

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, byte[] RowVersion)
        {
            var existing = await _turSkabelonRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _turSkabelonRepository.DeleteAsync(existing);
            return NoContent();
            //// 1) Find og tjek eksistens
            //var turSkabelon = await _turSkabelonRepository.GetByIdAsync(id);
            //if (turSkabelon == null)
            //    throw new KeyNotFoundException($"Turskabelon med id {id} ikke fundet.");

            //// 2) Sæt RowVersion til det, klienten kom med
            //turSkabelon.RowVersion = originalRowVersion;

            //// 3) Kald repository og fang concurrency–fejl
            //try
            //{
            //    await _turSkabelonRepository.DeleteAsync(turSkabelon);
            //}
            //catch (Exception)
            //{
            //    throw new(
            //        $"Turskabelon med id {id} blev enten slettet eller ændret af en anden. Genindlæs og prøv igen.");
            //}
        }


    }
}
