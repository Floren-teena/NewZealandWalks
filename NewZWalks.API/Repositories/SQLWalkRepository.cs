using Microsoft.EntityFrameworkCore;
using NewZWalks.API.Data;
using NewZWalks.API.Models.Domain;

namespace NewZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NewZWalksDbContext _dbContext;

        public SQLWalkRepository(NewZWalksDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllWalkAsync()
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

    }
}
