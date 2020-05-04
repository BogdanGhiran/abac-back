using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XPAND.Planets.API.Models;
using XPAND.Planets.API.Repositories;

namespace XPAND.Planets.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CrewMetaDataController : ControllerBase
    {
        private readonly ICrewMetaDataRepository _crewMetaDataRepository;

        public CrewMetaDataController(ICrewMetaDataRepository crewMetaDataRepository)
        {
            _crewMetaDataRepository = crewMetaDataRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CrewMetaData viewModel)
        {
            try
            {
                var planet = new CrewMetaData
                {
                    CaptainName = viewModel.CaptainName,
                    CaptainIdentifier = viewModel.CaptainIdentifier,
                    RobotList = viewModel.RobotList
                };
                await _crewMetaDataRepository.Create(planet);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}