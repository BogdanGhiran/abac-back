using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XPAND.Captains.API.Models
{
    public partial class Captain
    {
        public Captain()
        {
            Shuttles = new HashSet<Shuttle>();
        }

        [Key]
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid Identifier { get; set; }

        public virtual ICollection<Shuttle> Shuttles { get; set; }
    }
}
