using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.Common.Validation;

public class NoHtmlAttribute : ValidationAttribute
{
    public NoHtmlAttribute()
    {
        ErrorMessage = "El campo no puede contener HTML o etiquetas.";
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        if (value is not string text)
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            return true;
        }

        return !ContainsHtml(text);
    }

    private static bool ContainsHtml(string value)
    {
        return value.Contains('<') ||
               value.Contains('>') ||
               value.Contains("&lt;", StringComparison.OrdinalIgnoreCase) ||
               value.Contains("&gt;", StringComparison.OrdinalIgnoreCase);
    }
}