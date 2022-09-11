namespace StorEsc.Infrastructure.ExternalServices.Responses;

public class PaymentRechargeResponse
{
    public bool IsPaid { get; set; }
    public string Hash { get; set; }
    public DateTime CreatedAt { get; private set; }

    public PaymentRechargeResponse()
    {
        IsPaid = false;
        Hash = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    public PaymentRechargeResponse(bool isPaid)
    {
        IsPaid = isPaid;
        Hash = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }
    
    public PaymentRechargeResponse(bool isPaid, string hash)
    {
        IsPaid = isPaid;
        Hash = hash;
        CreatedAt = DateTime.UtcNow;
    }
}