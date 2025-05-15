using AutoMapper;
using TANE.Skabelon.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.GenericRepositories;

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
            var turSkabelon = await _turSkabelonRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TurSkabelonReadDto>>(turSkabelon));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<TurSkabelonReadDto>>> GetById(int id)
        {
            var turSkabelon = await _turSkabelonRepository.GetByIdWithIncludeAsync(id);
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
                return BadRequest();

            var turSkabelon = await _turSkabelonRepository.GetByIdWithIncludeAsync(
                id,include => include.Dage);
            if (turSkabelon == null)
                throw new KeyNotFoundException($"Tur {id} ikke fundet.");
            try
            {
               var ts = _mapper.Map(dto, turSkabelon);
                await _turSkabelonRepository.UpdateAsync(ts);
                return NoContent();
            }

            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"The entity {typeof(BaseEntity).Name} was modified by another user.");
            }
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
