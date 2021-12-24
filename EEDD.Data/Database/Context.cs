global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEDD.Data.Database
{
    public class Context : DbContext
    {
        public DbSet<Station> Stations { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<StationConnection> Connections { get; set; }
    }
}
