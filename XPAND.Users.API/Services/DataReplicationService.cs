using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers;
using XPAND.Captains.API.Models;

namespace XPAND.Captains.API.Services
{
    public class DataReplicationService: IDataReplicationService
    {
        public async Task RegisterCaptainInPlanetMicroservice(Captain captain, Shuttle shuttle)
        {
            var uri = new Uri("http://localhost:56521/crewMetaData/");
            await HttpHelper.PostAsync(uri, new
            {
                CaptainIdentifier = captain.Identifier,
                CaptainName = captain.FirstName + " " + captain.LastName,
                RobotList = string.Join(",", shuttle.Robots.Select(x => x.Name)),
            });
        }
    }
}
