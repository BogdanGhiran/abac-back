using System.Threading.Tasks;
using XPAND.Captains.API.Models;

namespace XPAND.Captains.API.Services
{
    public interface IShuttleService
    {
        Task<Shuttle> CreateShuttleWithCrew(string shuttleName, int captainID);
    }
}
