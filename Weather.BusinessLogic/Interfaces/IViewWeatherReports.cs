using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.BusinessLogic.Classes;

namespace Weather.BusinessLogic.Interfaces {
    public interface IViewWeatherReports {
        Task<IEnumerable<WeatherReport>> GetLastXWeatherReports(int x);
        Task<IEnumerable<WeatherReport>> GetAllWeatherReports(int take, int skip);
        Task<IEnumerable<WeatherReport>> GetGoodWeatherDays(int take, int skip, string order = "ASC");
        Task<WeatherReport> GetWeatherReport();
    }
}
