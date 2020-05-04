using System.Threading.Tasks;
using XPAND.Captains.API.Models;

namespace XPAND.Captains.API.Services
{
    public interface IDataReplicationService
    {
        public Task RegisterCaptainInPlanetMicroservice(Captain captain, Shuttle shuttle);
    }
}
