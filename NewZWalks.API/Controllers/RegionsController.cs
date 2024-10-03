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
            var regions = newZWalksDb.Regions.ToList();
            var regionDto = new List<RegionDTO>()
            {

            };

            return Ok(regions);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regionById = newZWalksDb.Regions.FirstOrDefault(i => i.Id == id);
            //var reg = newZWalksDb.Regions.Find(id);
            if (regionById == null)
            {
                return NotFound();
            }
            return Ok(regionById);
        }

    }
}
