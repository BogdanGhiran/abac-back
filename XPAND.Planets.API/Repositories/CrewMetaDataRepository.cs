using Common.GenericRepository;
using XPAND.Planets.API.Models;

namespace XPAND.Planets.API.Repositories
{
    public class CrewMetaDataRepository: GenericRepository<CrewMetaData>, ICrewMetaDataRepository
    {
        public CrewMetaDataRepository(AbacTestPlanetsContext dbContext) : base(dbContext)
        {
        }
    }
}
