using System.ComponentModel.DataAnnotations;
using WaterLogger.Validation;

namespace WaterLogger.Models;

public class DrinkingWater
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Date is required")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [Range(0, 30, ErrorMessage = "Value between 0 and 30 required")]
    public double Quantity { get; set; }

    [Required] [AllowedWordsOnly] public string Size { get; set; }
}