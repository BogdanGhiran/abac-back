using Microsoft.EntityFrameworkCore;

namespace XPAND.Planets.API.Models
{
    public partial class AbacTestPlanetsContext : DbContext
    {
        public AbacTestPlanetsContext()
        {
        }

        public AbacTestPlanetsContext(DbContextOptions<AbacTestPlanetsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CrewMetaData> CrewMetaData { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:bujde.database.windows.net,1433;Initial Catalog=abacTest_Planets;Persist Security Info=False;User ID=bujde;Password=abacTest123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrewMetaData>(entity =>
            {
                entity.HasKey(e => e.CaptainIdentifier);

                entity.Property(e => e.CaptainIdentifier).ValueGeneratedNever();

                entity.Property(e => e.CaptainName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.RobotList).IsRequired();
            });

            modelBuilder.Entity<Planet>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.Property(e => e.SolarSystem).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.VisitingCaptainId).HasColumnName("VisitingCaptainID");

                entity.HasOne(d => d.VisitingCrew)
                    .WithMany(p => p.Planets)
                    .HasForeignKey(d => d.VisitingCaptainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Planets_CrewMetaData");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
