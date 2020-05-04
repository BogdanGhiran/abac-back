using System;
using System.Threading.Tasks;
using XPAND.Planets.API.ViewModels;

namespace XPAND.Planets.API.Services
{
    public interface IPlanetService
    {
        Task UpdatePlanetAfterVisit(UpdatePlanetViewModel planetModel);
        Task SetRouteToPlanet(int planetId, Guid captainGuid);
    }
}
