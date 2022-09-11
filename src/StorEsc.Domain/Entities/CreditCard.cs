namespace StorEsc.Domain.Entities;

public class CreditCard
{
    public string HoldName { get; private set; }
    public string Number { get; private set; }
    public string ExpirationDate { get; private set; }
    public int CVV { get; private set; }
    public string Document { get; private set; }
    
    public CreditCard(
        string holdName,
        string number,
        string expirationDate,
        int cvv,
        string document)
    {
        HoldName = holdName;
        Number = number;
        ExpirationDate = expirationDate;
        CVV = cvv;
        Document = document;
    }
}