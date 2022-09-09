namespace StorEsc.Domain.Entities;

public class CreditCard
{
    public CreditCard(string holdName, string number, string validate, int cvv)
    {
        HoldName = holdName;
        Number = number;
        Validate = validate;
        CVV = cvv;
    }

    public string HoldName { get; private set; }
    public string Number { get; private set; }
    public string Validate { get; private set; }
    public int CVV { get; private set; }
}