using Microsoft.EntityFrameworkCore;
using Weather.BusinessLogic.Interfaces;
using Weather.BusinessLogic.Classes;

namespace Weather.Plugins.EFCore.Plugins {
    public class WeatherReportRepository : IWeatherReportRepository {
        private readonly WeatherContext _context;
        public WeatherReportRepository(WeatherContext context) {
            _context = context;
        }
        public async Task<IEnumerable<WeatherReport>> GetLastXWeatherReportsAsync(int x) {
            return await _context.WeatherReports.OrderByDescending(ii => ii.Id).Take(x).ToListAsync();
        }

        public async Task<IEnumerable<WeatherReport>> GetAllWeatherReportsAsync(int take, int skip) {
            if (take == -1)
                return await _context.WeatherReports.Skip(skip).ToListAsync();
            else
                return await _context.WeatherReports.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<WeatherReport>> GetGoodWeatherDays(int take, int skip, string order = "ASC") {
            switch (order.ToUpper()) {
                case "DESC":
                    if (take == -1)
                        return await _context.WeatherReports.Where(report => report.Temperature > 18 && report.CloudCover < 30 && report.AirHumidity < 60 && report.WindSpeed < 12).OrderByDescending(ii => ii.Id).Skip(skip).ToListAsync();
                    else
                        return await _context.WeatherReports.Where(report => report.Temperature > 18 && report.CloudCover < 30 && report.AirHumidity < 60 && report.WindSpeed < 12).OrderByDescending(ii => ii.Id).Skip(skip).Take(take).ToListAsync();
                default:
                    if (take == -1)
                        return await _context.WeatherReports.Where(report => report.Temperature > 18 && report.CloudCover < 30 && report.AirHumidity < 60 && report.WindSpeed < 12).OrderBy(ii => ii.Id).Skip(skip).ToListAsync();
                    else
                        return await _context.WeatherReports.Where(report => report.Temperature > 18 && report.CloudCover < 30 && report.AirHumidity < 60 && report.WindSpeed < 12).OrderBy(ii => ii.Id).Skip(skip).Take(take).ToListAsync();
            }
            
        }

        public async Task<Task> SaveWeatherReport(WeatherReport weatherReport) {
            try {
                await _context.WeatherReports.AddAsync(weatherReport);
                await _context.SaveChangesAsync();
                return Task.CompletedTask;
            } catch (Exception) {

                throw;
            }
        }
    }
}
