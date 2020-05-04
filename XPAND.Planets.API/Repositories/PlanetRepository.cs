using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.GenericRepository;
using Microsoft.EntityFrameworkCore;
using MoreLinq.Extensions;
using XPAND.Planets.API.Models;

namespace XPAND.Planets.API.Repositories
{
    public class PlanetRepository: GenericRepository<Planet>, IPlanetRepository
    {
        public PlanetRepository(AbacTestPlanetsContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Planet>> GetPlanetsWithVisitingCrews()
        {
            return await _dbContext.Set<Planet>().Include(x => x.VisitingCrew).ToListAsync();
        }

        public async Task<Planet> GetPlanetWithVisitingCrew(int id)
        {
            return await _dbContext.Set<Planet>().Include("VisitingCrew").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task GenerateInitialPlanets()
        {
            if (_dbContext.Set<Planet>().Any())
            {
                return;
            }

            var planetsToAdd = new List<Planet>();
            for (int i = 0; i < 30; i++)
            {
                Random rand = new Random();
                string[] greekLetters = {
                    "Alpha",
                    "Beta",
                    "Gamma",
                    "Delta",
                    "Epsilon",
                    "Zeta",
                    "Eta h",
                    "Theta",
                    "Iota",
                    "Kappa",
                    "Lambda",
                    "Mu",
                    "Nu",
                    "Xi",
                    "Omicron",
                    "Pi",
                    "Rho",
                    "Sigma",
                    "Tau",
                    "Upsilon",
                    "Phi",
                    "Chi",
                    "Psi",
                    "Omega",
                };

                string[] planetPictures = {
                    "https://img.icons8.com/pastel-glyph/2x/planet.png",
                    "https://img.icons8.com/cotton/2x/planet.png",
                    "https://img.icons8.com/ultraviolet/2x/planet.png",
                    "https://img.icons8.com/office/2x/planet.png",
                    "https://img.icons8.com/color/2x/planet.png",
                    "https://img.icons8.com/color/2x/jupiter-planet.png",
                    "https://img.icons8.com/color/2x/uranus-planet.png",
                    "https://img.icons8.com/color/2x/saturn-planet.png",
                    "https://img.icons8.com/cotton/2x/earth-planet.png",
                    "https://img.icons8.com/color/2x/earth-planet.png",
                    "https://img.icons8.com/color/2x/venus-planet.png",
                    "https://img.icons8.com/color/2x/neptune-planet.png",
                    "https://img.icons8.com/color/2x/mars-planet.png",
                    "https://icons8.com/icon/62036/pluto-dwarf-planet",
                    "https://img.icons8.com/cotton/2x/planet-on-the-dark-side.png",
                    "https://img.icons8.com/cotton/2x/planet-on-the-dark-side.png",
                    "https://img.icons8.com/cotton/2x/moon-satellite.png",
                    "https://img.icons8.com/color/2x/bricks-on-moon.png",
                };

                planetsToAdd.Add(new Planet
                {
                    Name= greekLetters[rand.Next(0,greekLetters.Length)] + " " + rand.Next(1,72),
                    Status= PlanetStatus.TODO,
                    ImageUrl = planetPictures[rand.Next(0, planetPictures.Length)],
                    Description = string.Empty,
                    SolarSystem = greekLetters[rand.Next(0, greekLetters.Length)],
                    VisitingCrew = null,
                    VisitingCaptainId = null
                });
            }

            planetsToAdd = planetsToAdd.DistinctBy(x => x.Name).ToList();

            await _dbContext.Set<Planet>().AddRangeAsync(planetsToAdd);
            await _dbContext.SaveChangesAsync();
        }
    }
}
