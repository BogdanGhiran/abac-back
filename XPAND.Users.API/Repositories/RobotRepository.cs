using Common.GenericRepository;
using XPAND.Captains.API.Models;

namespace XPAND.Captains.API.Repositories
{
    public class RobotRepository: GenericRepository<Robot>, IRobotRepository
    {
        public RobotRepository(AbacTestCaptainsContext dbContext) : base(dbContext)
        {
        }
    }
}
