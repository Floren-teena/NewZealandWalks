using Microsoft.EntityFrameworkCore;
using NewZWalks.API.Data;
using NewZWalks.API.Models.Domain;

namespace NewZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NewZWalksDbContext _dbContext;

        public SQLRegionRepository(NewZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public Task<Region> GetRegionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Region> CreateRegionAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region> UpdateRegionAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region> DeleteRegionAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}