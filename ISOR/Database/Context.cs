global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
using T = System.Timers;

namespace ISOR.Database
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

        public DbSet<Row> Rows { get; set; }

        public DbSet<Train> Trains { get; set; }

        public DbSet<TrainEvent> TrainHistories { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Stop> Stops { get; set; }

        public DbSet<Shift> Shifts { get; set; }

        public DbSet<Timetable> Timetables { get; set; }

        public DbSet<TimetableTrain> TimetableTrains { get; set; }

        public DbSet<TimetableStop> TimetableStops { get; set; }

        private Queue<LogEvent> events;
        private T.Timer timer;
        private bool externalConfig;

        public Context()
        {
            externalConfig = false;
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            externalConfig = true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                events = new();
                timer = new(1000);
                timer.Enabled = true;
                timer.Elapsed += FlushLog;
                optionsBuilder.LogTo(LogToFile);
            }
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\DebugDb.mdf;Integrated Security=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1250_CI_AS");
            modelBuilder.Entity<StationConnection>().HasOne(x => x.Primary).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<StationConnection>().HasOne(x => x.Secondary).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Stop>().HasOne(x => x.Track).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Stop>().HasOne(x => x.From).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Stop>().HasOne(x => x.To).WithOne().OnDelete(DeleteBehavior.NoAction);
        }

        private void LogToFile(string text)
        {
            LogEvent le = new(text);
            lock (events)
                events.Enqueue(le);
        }

        private void FlushLog(object? sender, T.ElapsedEventArgs e)
        {
            lock (events)
            {
                if (events.Count == 0)
                    return;

                try
                {
                    using (FileStream fs = new FileStream("dbLog.txt", FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            while (events.Count > 0)
                            {
                                var ev = events.Dequeue();
                                sw.WriteLine($"[{ev.Timestamp:dd.MM.yyyy HH:mm:ss:fff}]\t{ev.Text}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Couldn't flush DB log to file. Exception: " + ex.Message);
                }
            }
        }
    }

    internal record LogEvent
    {
        public DateTime Timestamp { get; init; }

        public string Text { get; set; }

        public LogEvent(string text)
        {
            Timestamp = DateTime.Now;
            Text = text;
        }
    }
}
