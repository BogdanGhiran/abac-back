using XPAND.Planets.API.Models;

namespace XPAND.Planets.API.ViewModels
{
    public class UpdatePlanetViewModel
    {
        public int Id { get; set; }
        
        public string Description { get; set; }

        public PlanetStatus PlanetStatus{ get; set; }
    }
}
