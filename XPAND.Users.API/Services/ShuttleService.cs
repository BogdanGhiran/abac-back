using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XPAND.Captains.API.Models;
using XPAND.Captains.API.Repositories;

namespace XPAND.Captains.API.Services
{
    public class ShuttleService: IShuttleService
    {
        private readonly IRobotRepository _robotRepository;
        private readonly IShuttleRepository _shuttleRepository;

        public ShuttleService(IRobotRepository robotRepository, IShuttleRepository shuttleRepository)
        {
            _robotRepository = robotRepository;
            _shuttleRepository = shuttleRepository;
        }

        public async Task<Shuttle> CreateShuttleWithCrew(string shuttleName, int captainId)
        {
            Random rnd = new Random();
            int crewSize = rnd.Next(2, 5);
            Shuttle shuttle = new Shuttle
            {
                CaptainId = captainId,
                Name = shuttleName
            };

            var robotList = await _robotRepository.GetAllQueryable().Where(x=>x.ShuttleId == null).Take(crewSize).ToListAsync();

            shuttle.Robots = robotList;

            await _shuttleRepository.Create(shuttle);

            return shuttle;
        }
    }
}
