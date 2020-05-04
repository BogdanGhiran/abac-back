using System.Collections.Generic;

namespace XPAND.Captains.API.Models
{
    public partial class Shuttle
    {
        public Shuttle()
        {
            Robots = new HashSet<Robot>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CaptainId { get; set; }

        public virtual Captain Captain { get; set; }
        public virtual ICollection<Robot> Robots { get; set; }
    }
}
