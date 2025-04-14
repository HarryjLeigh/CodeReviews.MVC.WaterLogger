using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages;

public class Update(IConfiguration configuration) : PageModel
{
    private readonly IConfiguration _configuration = configuration;

    [BindProperty] public DrinkingWater DrinkingWater { get; set; }

    public IActionResult OnGet(int id)
    {
        DrinkingWater = GetById(id);
        return Page();
    }

    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid)
        {
            DrinkingWater = GetById(id);
            return Page();
        }

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $@"UPDATE drinking_water 
                                      SET Date = '{DrinkingWater.Date}', 
                                          Quantity = '{Math.Round(DrinkingWater.Quantity, 2)}',
                                          Size = '{DrinkingWater.Size}'
                                      WHERE Id = '{DrinkingWater.Id}'";
            tableCmd.ExecuteNonQuery();
        }

        return RedirectToPage("./Index");
    }

    private DrinkingWater GetById(int id)
    {
        var drinkingWater = new DrinkingWater();
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM drinking_water Where Id = '{id}'";
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                drinkingWater.Id = reader.GetInt32(0);
                drinkingWater.Date = DateTime.ParseExact(reader.GetString(1), "dd/MM/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture);
                drinkingWater.Quantity = double.Parse(reader.GetString(2));
                drinkingWater.Size = reader.GetString(3);
            }
        }

        return drinkingWater;
    }
}