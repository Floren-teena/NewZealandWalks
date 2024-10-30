using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZWalks.API.Models.Domain;
using NewZWalks.API.Models.DTO;
using NewZWalks.API.Repositories;

namespace NewZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        [HttpPost]
        [Route("Add-walk")]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map DTO to Domain model
            var walkDomain = _mapper.Map<Walk>(addWalkRequestDto);

            await _walkRepository.CreateWalkAsync(walkDomain);

            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }

        [HttpGet]
        [Route("Get-all-walks")]
        public async Task<IActionResult> GetAll()
        {
            var walkDomain = await _walkRepository.GetAllWalkAsync();

            return Ok(_mapper.Map<List<WalkDto>>(walkDomain));
        }

        [HttpGet]
        [Route("Get-walk-by-id/{id:Guid}")]
        public async Task<IActionResult> GetByid([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.GetWalkByidAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }
    }
}
