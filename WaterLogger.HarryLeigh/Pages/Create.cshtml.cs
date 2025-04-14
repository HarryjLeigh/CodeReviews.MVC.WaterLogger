using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages;

public class CreateModel(IConfiguration configuration) : PageModel
{
    private readonly IConfiguration _configuration = configuration;

    [BindProperty] public DrinkingWater DrinkingWater { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var tableCommand = connection.CreateCommand();
            tableCommand.CommandText =
                $@"INSERT INTO drinking_water (Date, Quantity, Size)
                   VALUES ('{DrinkingWater.Date}', '{Math.Round(DrinkingWater.Quantity, 2)}', 
                           '{DrinkingWater.Size.ToLower()}');";
            tableCommand.ExecuteNonQuery();
        }

        return RedirectToPage("./Index");
    }
}