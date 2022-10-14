using System.ComponentModel.DataAnnotations;

namespace StorEsc.API.Attributes.Validation;

public class DocumentAttribute: ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var document = value as string;

        if (document.Length == 11 || document.Length == 14)
            return true;

        return false;
    }
}