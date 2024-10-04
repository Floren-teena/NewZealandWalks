using NewZWalks.API.Models.Domain;

namespace NewZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<Region> GetRegionByIdAsync(int id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region> UpdateRegionAsync(Region region);
        Task<Region> DeleteRegionAsync(int id);
    }
}
