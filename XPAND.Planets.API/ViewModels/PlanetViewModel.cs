using System;
using XPAND.Planets.API.Models;

namespace XPAND.Planets.API.ViewModels
{
    public class PlanetViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PlanetStatus Status { get; set; }

        public string SolarSystem { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public Guid? VisitingCaptainId { get; set; }
    }
}
