using System.ComponentModel.DataAnnotations;

namespace StorEsc.Api.Attributes.Validation;

public class ExpirationDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime expirationDate;
        string expirationDateFormatted = "01/" + value as string;

        if (DateTime.TryParse(expirationDateFormatted, out expirationDate))
            if (expirationDate > DateTime.UtcNow)
                return true;

        return false;
    }
}