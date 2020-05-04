using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XPAND.Planets.API.Models
{
    public partial class CrewMetaData
    {
        public CrewMetaData()
        {
            Planets = new HashSet<Planet>();
        }

        public Guid CaptainIdentifier { get; set; }
        public string CaptainName { get; set; }
        public string RobotList { get; set; }

        [JsonIgnore]
        public virtual ICollection<Planet> Planets { get; set; }
    }
}
