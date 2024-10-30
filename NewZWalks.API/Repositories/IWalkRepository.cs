using NewZWalks.API.Models.Domain;

namespace NewZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<List<Walk>> GetAllWalkAsync();
        Task<Walk?> GetWalkByidAsync(Guid id);
    }
}
