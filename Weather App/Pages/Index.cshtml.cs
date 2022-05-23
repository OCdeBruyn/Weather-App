using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Weather.BusinessLogic.Interfaces;
using Weather.BusinessLogic.Services;
using Weather.BusinessLogic.Classes;
using System.Linq;

namespace Weather_App.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        private IViewWeatherReports _weatherReportRepository;
        public WeatherReport lastReport = new WeatherReport();
        public string reportImage = string.Empty;
        const string red = "#EE4B2B";
        const string grey = "#808080";
        public string temperatureColour = grey;
        public string cloudCoverColour = grey;
        public string airHumidityColour = grey;
        public string windSpeedColour = grey;

        public IndexModel(ILogger<IndexModel> logger, IViewWeatherReports weatherReportRepository) {
            _logger = logger;
            _weatherReportRepository = weatherReportRepository;
        }

        public async Task OnGet() {
            var response = await _weatherReportRepository.GetLastXWeatherReports(1);
            var report = response.FirstOrDefault();
            if (report != null)
                setLastReport(report);
        }

        public async Task OnPostButton() {
            var report = await _weatherReportRepository.GetWeatherReport();
            setLastReport(report);
        }

        private void setLastReport(WeatherReport report) {
            lastReport = report;
            temperatureColour = report.Temperature > 18 ? grey : red;
            cloudCoverColour = report.CloudCover < 30 ? grey : red;
            airHumidityColour = report.AirHumidity < 60 ? grey : red;
            windSpeedColour = report.WindSpeed < 12 ? grey : red;
            if (report.Temperature > 18 && report.CloudCover < 30 && report.AirHumidity < 60 && report.WindSpeed < 12) {
                reportImage = "https://media4.giphy.com/media/MNmyTin5qt5LSXirxd/giphy.gif?cid=ecf05e47pvdqkbzqkciesxfqz00u09d5uuy5kik38rqbrwjv&rid=giphy.gif&ct=g";
            } else {
                reportImage = "https://media3.giphy.com/media/xTcnTehwgRcbgymhTW/giphy.gif?cid=ecf05e47lkr1wl44lzzcembvmbc4s13t3bzbq0dg1hysbg4c&rid=giphy.gif&ct=g";
            }
        }
    }
}