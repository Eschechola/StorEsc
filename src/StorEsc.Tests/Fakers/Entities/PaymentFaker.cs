using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class PaymentFaker : IFaker<Payment>
{
    public Payment GetValid()
        => new Payment(
            isPaid: true,
            hash: Guid.NewGuid().ToString());

    public Payment GetInvalid()
        => new Payment(
            isPaid: false,
            hash: "");
}