using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class PaymentFaker : BaseFaker<Payment>
{
    public override Payment GetValid()
        => new Payment(
            isPaid: true,
            hash: Guid.NewGuid().ToString());

    public Payment GetValid(bool isPaid)
        => new Payment(
            isPaid: isPaid,
            hash: Guid.NewGuid().ToString());

    public override Payment GetInvalid()
        => new Payment(
            isPaid: false,
            hash: "");
}