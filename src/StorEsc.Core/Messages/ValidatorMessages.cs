namespace StorEsc.Core.Messages;

public static class ValidatorMessages
{
    public static string NotNull(string propertyName)
        => $"{propertyName} can not be null.";
    
    public static string NotEmpty(string propertyName)
        => $"{propertyName} can not be empty.";
    
    public static string MinimumLength(string propertyName, int minimumLenght)
        => $"{propertyName} can have a minimum of {minimumLenght} characters.";
    
    public static string MaximumLength(string propertyName, int maximumLenght)
        => $"{propertyName} can have a maximum of {maximumLenght} characters.";

    public static string GreaterThanOrEqualTo(string propertyName, double value)
        => $"{propertyName} can be greater than or equals to ${value}";
}