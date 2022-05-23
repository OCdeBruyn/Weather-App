using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.BusinessLogic.Classes;

namespace Weather.Plugins.EFCore {
    public class WeatherContext : DbContext {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }

        public DbSet<WeatherReport> WeatherReports { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder) {

        }
    }
}
