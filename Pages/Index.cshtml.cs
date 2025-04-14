using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages;

public class IndexModel(IConfiguration configuration) : PageModel
{
    private readonly IConfiguration _configuration = configuration;
    public List<DrinkingWater> Records { get; set; }

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<DrinkingWater> GetAllRecords()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            var tableData = new List<DrinkingWater>();
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = "SELECT * FROM drinking_water";
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
                tableData.Add(new DrinkingWater
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.ParseExact(reader.GetString(1), "dd/MM/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture),
                    Quantity = double.Parse(reader.GetString(2)),
                    Size = CapitalizeFirstWord(reader.GetString(3))
                });

            return tableData;
        }
    }

    private string CapitalizeFirstWord(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;

        input = input.Trim();
        return char.ToUpper(input[0]) + input.Substring(1);
    }
}