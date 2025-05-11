using AutoMapper;
using TANE.Skabelon.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.GenericRepositories;
using TANE.Skabelon.Api.Models;


namespace TANE.Skabelon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DagSkabelonController : ControllerBase
    {
        private readonly IGenericRepository<DagSkabelonModel> _dagSkabelonRepository;
        private readonly IMapper _mapper;

        public DagSkabelonController(IGenericRepository<DagSkabelonModel> genericRepository, IMapper mapper)
        {
            _dagSkabelonRepository = genericRepository;
            _mapper = mapper;
        }
        //Søren revies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DagSkabelonReadDto>>> GetAll()
        {
            var dagSkabeloner = await _dagSkabelonRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<DagSkabelonReadDto>>(dagSkabeloner));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DagSkabelonReadDto>> GetById(int id)
        {
            var dagSkabeloner = await _dagSkabelonRepository.GetByIdAsync(id);
            if (dagSkabeloner == null)
                return NotFound();
            return Ok(_mapper.Map<DagSkabelonReadDto>(dagSkabeloner));
        }

        //Søren review
        [HttpPost]
        public async Task<ActionResult<DagSkabelonReadDto>> Create([FromBody] DagSkabelonCreateDto dto)
        {
            var dagSkabelonEntity = _mapper.Map<DagSkabelonModel>(dto);
            await _dagSkabelonRepository.AddAsync(dagSkabelonEntity);

            var readDto = _mapper.Map<DagSkabelonReadDto>(dagSkabelonEntity);
            return CreatedAtAction(nameof(GetById),new { id = readDto.Id}, _mapper.Map<DagSkabelonReadDto> (readDto));
            
        }
        //Søren review
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] DagSkabelonUpdateDto dto)
        {
            var existing = await _dagSkabelonRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

           var ds = _mapper.Map(dto, existing);
            await _dagSkabelonRepository.UpdateAsync(ds);
            return NoContent();
        }
        //Søren review
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] byte[] rowVersion)
        {
            var existing = await _dagSkabelonRepository.GetByIdAsync(id);
            if ( existing == null)
                return NotFound();

            await _dagSkabelonRepository.DeleteAsync(existing);
                return NoContent();
        }


        //public async Task DeleteDagSkabelonModelAsync(int id, byte[] originalRowVersion)
        //{
        //    // 1) Find og tjek eksistens
        //    var dagSkabelon = await _dagSkabelonRepository.GetByIdAsync(id);
        //    if (dagSkabelon == null)
        //        throw new KeyNotFoundException($"Dagskabelon med id {id} ikke fundet.");

        //    // 2) Sæt RowVersion til det, klienten kom med
        //    dagSkabelon.RowVersion = originalRowVersion;

        //    // 3) Kald repository og fang concurrency–fejl
        //    try
        //    {
        //        await _dagSkabelonRepository.DeleteAsync(dagSkabelon);
        //    }
        //    catch (Exception)
        //    {
        //        throw new(
        //            $"Dagskabelon med id {id} blev enten slettet eller ændret af en anden. Genindlæs og prøv igen.");
        //    }
        //}


    }
}
