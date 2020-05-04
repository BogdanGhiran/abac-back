using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XPAND.Planets.API.Models;
using XPAND.Planets.API.Repositories;
using XPAND.Planets.API.Services;
using XPAND.Planets.API.ViewModels;

namespace XPAND.Planets.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanetController : ControllerBase
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly IPlanetService _planetService;

        public PlanetController(IPlanetRepository planetRepository, IPlanetService planetService)
        {
            _planetRepository = planetRepository;
            _planetService = planetService;
        }

        [HttpGet]
        public async Task<List<Planet>> Get()
        {
            return await _planetRepository.GetPlanetsWithVisitingCrews();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Planet> GetById(int id)
        {
            var planetWithVisitingCrew =  await _planetRepository.GetPlanetWithVisitingCrew(id);

            return planetWithVisitingCrew;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _planetRepository.Delete(id);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdatePlanetViewModel viewModel)
        {
            await _planetService.UpdatePlanetAfterVisit(viewModel);

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> SetRouteToPlanet([FromBody]SetPlanetRouteViewModel viewModel)
        {
            await _planetService.SetRouteToPlanet(viewModel.Id, viewModel.CaptainIdentifier);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Planet planetViewModel)
        {
            try
            {
                var planet = new Planet
                {
                    Name = planetViewModel.Name,
                    Description = string.Empty,
                    ImageUrl = planetViewModel.ImageUrl,
                    SolarSystem = planetViewModel.SolarSystem,
                    Status = PlanetStatus.TODO
                };
                await _planetRepository.Create(planet);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
