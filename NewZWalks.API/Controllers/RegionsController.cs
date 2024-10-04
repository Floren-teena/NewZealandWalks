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
        private readonly NewZWalksDbContext newZWalksDb;
        private readonly IRegionRepository _regionRepository;

        public RegionsController(NewZWalksDbContext newZWalksDb, IRegionRepository regionRepository)
        {
            this.newZWalksDb = newZWalksDb;
            _regionRepository = regionRepository;
        }

        [HttpGet]
        [Route("Get-all-regions")]
        public async Task<IActionResult> GetAll()
        {
            var regionDomain = await _regionRepository.GetAllRegionsAsync();
            var regionDto = new List<RegionDto>();
            foreach (var region in regionDomain) 
            {
                regionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("Get-region-by-id/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await newZWalksDb.Regions.FirstOrDefaultAsync(i => i.Id == id);
            //var reg = newZWalksDb.Regions.Find(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }

        [HttpPost]
        [Route("Create-Region")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomain = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            await newZWalksDb.AddAsync(regionDomain);
            await newZWalksDb.SaveChangesAsync();
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("Update-region/{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomain = await newZWalksDb.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            regionDomain.Name = updateRegionRequestDto.Name;
            regionDomain.Code = updateRegionRequestDto.Code;
            regionDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await newZWalksDb.SaveChangesAsync();

            var updatedRegionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(updatedRegionDto);
        }

        [HttpDelete]
        [Route("Delete-region/{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await newZWalksDb.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (regionDomain == null) 
            { 
                return NotFound(); 
            }

            newZWalksDb.Remove(regionDomain);
            await newZWalksDb.SaveChangesAsync();

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }
    }
}
