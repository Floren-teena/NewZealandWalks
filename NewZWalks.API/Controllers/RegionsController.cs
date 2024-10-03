using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZWalks.API.Data;
using NewZWalks.API.Models.Domain;
using NewZWalks.API.Models.DTO;

namespace NewZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NewZWalksDbContext newZWalksDb;

        public RegionsController(NewZWalksDbContext newZWalksDb)
        {
            this.newZWalksDb = newZWalksDb;
        }

        [HttpGet]
        [Route("Get-all-regions")]
        public IActionResult GetAll()
        {
            var regionDomain = newZWalksDb.Regions.ToList();
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
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regionDomain = newZWalksDb.Regions.FirstOrDefault(i => i.Id == id);
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
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomain = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            newZWalksDb.Add(regionDomain);
            newZWalksDb.SaveChanges();
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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomain = newZWalksDb.Regions.FirstOrDefault(r => r.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            regionDomain.Name = updateRegionRequestDto.Name;
            regionDomain.Code = updateRegionRequestDto.Code;
            regionDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            newZWalksDb.SaveChanges();

            var updatedRegionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(updatedRegionDto);
        }
    }
}
