using System;

namespace XPAND.Planets.API.Models
{
    public partial class Planet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PlanetStatus Status { get; set; }

        public string SolarSystem { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public Guid? VisitingCaptainId { get; set; }

        public virtual CrewMetaData VisitingCrew { get; set; }
    }
}
