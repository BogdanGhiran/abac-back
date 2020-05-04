using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers;
using Microsoft.Extensions.Configuration;
using XPAND.Captains.API.Models;

namespace XPAND.Captains.API.Services
{
    public class DataReplicationService: IDataReplicationService
    {
        private readonly IConfiguration _config;

        public DataReplicationService(IConfiguration config)
        {
            _config = config;
        }

        public async Task RegisterCaptainInPlanetMicroservice(Captain captain, Shuttle shuttle)
        {
            var uri = new Uri(_config["PlanetAPIUrl"] +"crewMetaData/");
            await HttpHelper.PostAsync(uri, new
            {
                CaptainIdentifier = captain.Identifier,
                CaptainName = captain.FirstName + " " + captain.LastName,
                RobotList = string.Join(",", shuttle.Robots.Select(x => x.Name)),
            });
        }
    }
}
