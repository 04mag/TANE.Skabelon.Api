
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
    public class RejseplanSkabelonController : ControllerBase
    {
        private readonly IGenericRepository<RejseplanSkabelonModel> _rejseplanSkabelonRepository;
        private readonly IMapper _mapper;

        public RejseplanSkabelonController(IGenericRepository<RejseplanSkabelonModel> genericRepository, IMapper mapper)
        {
            _rejseplanSkabelonRepository = genericRepository;
            _mapper = mapper;
        }

        [HttpGet("read")]
        public async Task<ActionResult<IEnumerable<RejseplanSkabelonReadDto>>> GetAll()
        {
            var rejseplanSkabelon = await _rejseplanSkabelonRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RejseplanSkabelonReadDto>>(rejseplanSkabelon));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RejseplanSkabelonReadDto>> GetById(int id)
        {
            var rejseplanSkabelon = await _rejseplanSkabelonRepository.GetByIdAsync(id);
            if (rejseplanSkabelon == null)
                return NotFound();
            return Ok(_mapper.Map<RejseplanSkabelonReadDto>(rejseplanSkabelon));
        }

        [HttpPost("create")]
        public async Task<ActionResult<RejseplanSkabelonReadDto>> Create(RejseplanSkabelonCreateDto rejseplanSkabelonCreateDto)
        {
            var rejseplanSkabelon = _mapper.Map<RejseplanSkabelonModel>(rejseplanSkabelonCreateDto);
            await _rejseplanSkabelonRepository.AddAsync(rejseplanSkabelon);

            return CreatedAtAction(nameof(GetById), new {id = rejseplanSkabelon.Id},
                _mapper.Map<RejseplanSkabelonReadDto>(rejseplanSkabelon));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RejseplanSkabelonUpdateDto rejseplanSkabelonUpdateDto)
        {
            if (id != rejseplanSkabelonUpdateDto.Id)
                return BadRequest();

            var rejseplanSkabelon = await _rejseplanSkabelonRepository.GetByIdAsync(id);
            if (rejseplanSkabelon == null)
                return NotFound();

            _mapper.Map(rejseplanSkabelonUpdateDto, rejseplanSkabelon);
            await _rejseplanSkabelonRepository.UpdateAsync(id, rejseplanSkabelonUpdateDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            var rejseplanSkabelon = await _rejseplanSkabelonRepository.GetByIdAsync(id);
            if (rejseplanSkabelon == null)
                return NotFound();

            return NoContent();
        }
        //public async Task DeleteRejseplanSkabelonModelAsync(int id, byte[] originalRowVersion)
        //{
        //    // 1) Find og tjek eksistens
        //    var rejseplanSkabelon = await _rejseplanSkabelonRepository.GetByIdAsync(id);
        //    if (rejseplanSkabelon == null)
        //        throw new KeyNotFoundException($"Rejseplanskabelon med id {id} ikke fundet.");

        //    // 2) Sæt RowVersion til det, klienten kom med
        //    rejseplanSkabelon.RowVersion = originalRowVersion;

        //    // 3) Kald repository og fang concurrency–fejl
        //    try
        //    {
        //        await _rejseplanSkabelonRepository.DeleteAsync(rejseplanSkabelon);
        //    }
        //    catch (Exception)
        //    {
        //        throw new(
        //            $"Rejseplanskabelon med id {id} blev enten slettet eller ændret af en anden. Genindlæs og prøv igen.");
        //    }
        //}
    }
 }
    
