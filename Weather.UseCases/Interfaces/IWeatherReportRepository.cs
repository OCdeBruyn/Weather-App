using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Classes;

namespace Weather.UseCases.Interfaces {
    public interface IWeatherReportRepository {
        Task<IEnumerable<WeatherReport>> GetLastXWeatherReportsAsync(int x);
        Task<IEnumerable<WeatherReport>> GetAllWeatherReportsAsync();

        Task<Task> SaveWeatherReport(WeatherReport weatherReport);
    }
}
