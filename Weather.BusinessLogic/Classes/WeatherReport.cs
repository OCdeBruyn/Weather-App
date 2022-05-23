using System.ComponentModel.DataAnnotations.Schema;

namespace Weather.BusinessLogic.Classes {
    public class WeatherReport {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Temperature { get; set; }
        public int WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public int CloudCover { get; set; }
        public int AirHumidity { get; set; }
        public string Location { get; set; }
        public string WeatherDescription { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
    }
}
