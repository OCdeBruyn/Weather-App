using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Classes;

namespace Weather.UseCases.Interfaces {
    public interface IViewWeatherReports {
        Task<IEnumerable<WeatherReport>> GetLastXWeatherReports(int x);
        Task<IEnumerable<WeatherReport>> GetAllWeatherReports();
        Task<WeatherReport> GetWeatherReport(WeatherReport? lastReport);
    }
}
