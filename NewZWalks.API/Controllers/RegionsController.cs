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
        [Route("{id:Guid}")]
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
    }
}
