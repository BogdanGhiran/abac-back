using System.Collections.Generic;
using System.Threading.Tasks;
using Common.GenericRepository;
using XPAND.Planets.API.Models;

namespace XPAND.Planets.API.Repositories
{
    public interface IPlanetRepository : IGenericRepository<Planet>
    {
        Task<List<Planet>> GetPlanetsWithVisitingCrews();
        Task<Planet> GetPlanetWithVisitingCrew(int id);
        Task GenerateInitialPlanets();
    }
}
