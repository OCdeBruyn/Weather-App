using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.UseCases.Interfaces;
using Weather.Classes;

namespace Weather.UseCases.Services {
    public class WeatherReportService : IViewWeatherReports {
        private readonly IWeatherReportRepository _weatherReportRepository;
        private static readonly HttpClient client = new HttpClient();
        public WeatherReportService(IWeatherReportRepository weatherReportRepository) {
            _weatherReportRepository = weatherReportRepository;
        }

        public async Task<IEnumerable<WeatherReport>> GetLastXWeatherReports(int x) {
            return await _weatherReportRepository.GetLastXWeatherReportsAsync(x);
        }
        public async Task<IEnumerable<WeatherReport>> GetAllWeatherReports() {
            return await _weatherReportRepository.GetAllWeatherReportsAsync();
        }

        public async Task<WeatherReport> GetWeatherReport(WeatherReport? lastReport) {
            var report = new WeatherReport();
            client.DefaultRequestHeaders.Accept.Clear();
            var stringTask = client.GetStringAsync("https://wttr.in/Culemborg?format=j1");
            var msg = await stringTask;
            if (!string.IsNullOrWhiteSpace(msg)) {
                var data = (JObject)JsonConvert.DeserializeObject(msg);
                report.Temperature = data.SelectToken("current_condition[0].temp_C").Value<int>();
                report.WindSpeed = data.SelectToken("current_condition[0].windspeedKmph").Value<int>();
                report.WindDirection = data.SelectToken("current_condition[0].winddirDegree").Value<int>();
                report.CloudCover = data.SelectToken("current_condition[0].cloudcover").Value<int>();
                report.AirHumidity = data.SelectToken("current_condition[0].humidity").Value<int>();
                report.TimeOfMeasurement = data.SelectToken("current_condition[0].localObsDateTime").Value<DateTime>();
                report.Location = data.SelectToken("nearest_area[0].areaName[0].value").Value<string>();
                report.WeatherDescription = data.SelectToken("current_condition[0].weatherDesc[0].value").Value<string>();

                if (compareReports(report, lastReport))
                    await SaveWeatherReport(report);
            }
            return report;
        }

        private bool compareReports(WeatherReport report, WeatherReport? lastReport) {
            if (lastReport != null) {
                return report.Temperature == lastReport.Temperature &&
                        report.WindSpeed == lastReport.WindSpeed &&
                        report.WindDirection == lastReport.WindDirection &&
                        report.CloudCover == lastReport.CloudCover &&
                        report.AirHumidity == lastReport.AirHumidity &&
                        report.TimeOfMeasurement == lastReport.TimeOfMeasurement &&
                        report.Location == lastReport.Location &&
                        report.WeatherDescription == lastReport.WeatherDescription;
            } else return true;
        }

        public async Task<Task> SaveWeatherReport(WeatherReport weatherReport) {
            await _weatherReportRepository.SaveWeatherReport(weatherReport);
            return Task.CompletedTask;
        }
    }
}