namespace StorEsc.Application.Dtos;

public class CreditCardDto
{
    public string HoldName { get; set; }
    public string Number { get; set; }
    public string ExpirationDate { get; set; }
    public int CVV { get; set; }
    public string Document { get; set; }
}