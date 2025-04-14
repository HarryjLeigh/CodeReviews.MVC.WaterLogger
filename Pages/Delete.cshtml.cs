using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages;

public class Delete(IConfiguration configuration) : PageModel
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
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"DELETE from drinking_water WHERE Id = {id}";
            var rowsDeleted = tableCmd.ExecuteNonQuery();
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
                drinkingWater.Quantity = reader.GetInt32(2);
                drinkingWater.Size = reader.GetString(3);
            }
        }

        return drinkingWater;
    }
}