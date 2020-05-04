namespace XPAND.Captains.API.Models
{
    public partial class Robot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ShuttleId { get; set; }

        public virtual Shuttle Shuttle { get; set; }
    }
}
