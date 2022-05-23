using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather_App.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    WindDirection = table.Column<int>(type: "int", nullable: false),
                    CloudCover = table.Column<int>(type: "int", nullable: false),
                    AirHumidity = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeOfMeasurement = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherReports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherReports");
        }
    }
}
