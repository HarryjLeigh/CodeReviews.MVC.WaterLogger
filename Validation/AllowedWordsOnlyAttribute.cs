using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WaterLogger.Validation;

public class AllowedWordsOnlyAttribute : ValidationAttribute
{
    private static readonly string[] AllowedWords = { "big bottle", "bottle", "glass" };

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var input = value?.ToString()?.Trim().ToLower();
        if (input != null && !AllowedWords.Contains(input))
            return new ValidationResult(
                $"Only the following words are allowed: {string.Join(", ", ArrayToTitleCase(AllowedWords))}");
        return ValidationResult.Success;
    }

    private string[] ArrayToTitleCase(string[] phrases)
    {
        var capitaliseAllowedWords = new List<string>();
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        for (var i = 0; i < phrases.Length; i++) capitaliseAllowedWords.Add(textInfo.ToTitleCase(phrases[i]));
        return capitaliseAllowedWords.ToArray();
    }
}