using Microsoft.AspNetCore.Mvc;
using Weather.BusinessLogic.Classes;
using Weather.BusinessLogic.Interfaces;
using System.Linq;
using System;

namespace Weather.Api.Controllers {
    public class WeatherReportController : Controller {
        private IViewWeatherReports _viewWeatherReports;
        public WeatherReportController(IViewWeatherReports viewWeatherReports) {
            this._viewWeatherReports = viewWeatherReports;
        }
        [HttpGet]
        [Route("GetAllReports/{take}/{skip}")]
        public async Task<List<WeatherReport>> GetAllReports(int take, int skip) {
            var result = await _viewWeatherReports.GetAllWeatherReports(take, skip);
            return result.ToList();
        }

        [HttpGet]
        [Route("GetLastXReports/{x}")]
        public async Task<List<WeatherReport>> GetLastXReports(int x) {
            var result = await _viewWeatherReports.GetLastXWeatherReports(x);
            return result.ToList();
        }

        [HttpGet]
        [Route("GetGoodWeatherDays/{take}/{skip}/{order}")]
        public async Task<List<WeatherReport>> GetGoodWeatherDays(int take, int skip, string order = "ASC") {
            var result = await _viewWeatherReports.GetGoodWeatherDays(take, skip, order);
            return result.ToList();
        }
    }
}
