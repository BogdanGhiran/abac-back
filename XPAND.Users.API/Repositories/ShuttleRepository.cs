using Common.GenericRepository;
using XPAND.Captains.API.Models;

namespace XPAND.Captains.API.Repositories
{
    public class ShuttleRepository: GenericRepository<Shuttle>, IShuttleRepository
    {
        public ShuttleRepository(AbacTestCaptainsContext dbContext) : base(dbContext)
        {
        }
    }
}
