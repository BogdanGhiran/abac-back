using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ExceptionHandling;
using XPAND.Planets.API.Models;
using XPAND.Planets.API.Repositories;
using XPAND.Planets.API.ViewModels;

namespace XPAND.Planets.API.Services
{
    public class PlanetService: IPlanetService
    {
        private readonly IPlanetRepository _planetRepository;

        public PlanetService(IPlanetRepository planetRepository)
        {
            _planetRepository = planetRepository;
        }

        public async Task UpdatePlanetAfterVisit(UpdatePlanetViewModel planetModel)
        {

            var planet = await _planetRepository.GetById(planetModel.Id);

            planet.Description = planetModel.Description;
            planet.Status = planetModel.PlanetStatus;
            await _planetRepository.Update(planetModel.Id, planet);
        }

        public async Task SetRouteToPlanet(int planetId, Guid captainGuid)
        {
            Planet planet = await _planetRepository.GetById(planetId);

            var alreadyGoingSomewhere = _planetRepository.GetAllQueryable().Any(x=>x.VisitingCaptainId == captainGuid && x.Status == PlanetStatus.EnRoute);

            if (alreadyGoingSomewhere)
            {
                throw new AlreadyOnRouteException();
            }

            planet.VisitingCaptainId = captainGuid;
            planet.Status = PlanetStatus.EnRoute;
            await _planetRepository.Update(planetId, planet);
        }

        public async Task SetRouteForPlanet(UpdatePlanetViewModel planetModel)
        {

            var planet = await _planetRepository.GetById(planetModel.Id);

            planet.Description = planetModel.Description;
            planet.Status = planetModel.PlanetStatus;
            await _planetRepository.Update(planetModel.Id, planet);
        }
    }
}
