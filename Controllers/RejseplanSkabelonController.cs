
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
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RejseplanSkabelonReadDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RejseplanSkabelonReadDto>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<RejseplanSkabelonReadDto>> Create(RejseplanSkabelonCreateDto rejseplanSkabelonCreateDto)
        {
            throw new NotImplementedException();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RejseplanSkabelonUpdateDto rejseplanSkabelonUpdateDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
 }
    
