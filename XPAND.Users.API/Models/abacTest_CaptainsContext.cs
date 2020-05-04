using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace XPAND.Captains.API.Models
{
    public partial class AbacTestCaptainsContext : DbContext
    {
        private readonly IConfiguration _config;

        public AbacTestCaptainsContext(IConfiguration config)
        {
            _config = config;
        }

        public AbacTestCaptainsContext(DbContextOptions<AbacTestCaptainsContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        public virtual DbSet<Captain> Captains { get; set; }
        public virtual DbSet<Robot> Robots { get; set; }
        public virtual DbSet<Shuttle> Shuttles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config["JWTSecret"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Captain>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Robot>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ShuttleId).HasColumnName("ShuttleID");

                entity.HasOne(d => d.Shuttle)
                    .WithMany(p => p.Robots)
                    .HasForeignKey(d => d.ShuttleId)
                    .HasConstraintName("FK_Robots_Shuttles");
            });

            modelBuilder.Entity<Shuttle>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainId).HasColumnName("CaptainID");

                entity.Property(e => e.Name).HasMaxLength(300);

                entity.HasOne(d => d.Captain)
                    .WithMany(p => p.Shuttles)
                    .HasForeignKey(d => d.CaptainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shuttles_Captains");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
