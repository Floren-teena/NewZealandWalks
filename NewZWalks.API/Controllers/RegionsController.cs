using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewZWalks.API.Data;
using NewZWalks.API.Models.Domain;
using NewZWalks.API.Models.DTO;
using NewZWalks.API.Repositories;

namespace NewZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(NewZWalksDbContext newZWalksDb, IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Get-all-regions")]
        public async Task<IActionResult> GetAll()
        {
            var regionDomain = await _regionRepository.GetAllRegionsAsync();
            return Ok(_mapper.Map<List<RegionDto>>(regionDomain));
        }

        [HttpGet]
        [Route("Get-region-by-id/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetRegionByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        [Route("Create-Region")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomain = _mapper.Map<Region>(addRegionRequestDto);

            await _regionRepository.CreateRegionAsync(regionDomain);

            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("Update-region/{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomain = _mapper.Map<Region>(updateRegionRequestDto);

            regionDomain = await _regionRepository.UpdateRegionAsync(id, regionDomain);
            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }

        [HttpDelete]
        [Route("Delete-region/{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.DeleteRegionAsync(id);
            if (regionDomain == null) 
            { 
                return NotFound(); 
            }

            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }
    }
}
