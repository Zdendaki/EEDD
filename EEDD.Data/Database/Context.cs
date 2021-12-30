global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;

namespace ServerData.Database
{
    public class Context : DbContext
    {
        public DbSet<Station> Stations { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<StationConnection> Connections { get; set; }

        public DbSet<RouteTrack> RouteTracks { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Signaller> Signallers { get; set; }

        public DbSet<Archive> Archive { get; set; }

        public DbSet<Train> Trains { get; set; }

        public DbSet<Stop> Stops { get; set; }

        public DbSet<Shift> Shifts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=vps.zdendaki.net;Database=EEDD;User Id=edd;Password=HusSNne5Z5vVSqsz;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1250_CI_AS");
            modelBuilder.Entity<StationConnection>().HasOne(x => x.Primary).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<StationConnection>().HasOne(x => x.Secondary).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Stop>().HasOne(x => x.StationTrack).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Stop>().HasOne(x => x.From).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Stop>().HasOne(x => x.To).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
