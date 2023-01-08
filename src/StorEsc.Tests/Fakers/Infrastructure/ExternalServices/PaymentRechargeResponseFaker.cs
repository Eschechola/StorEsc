using StorEsc.Infrastructure.ExternalServices.Responses;

namespace StorEsc.Tests.Fakers.Infrastructure.ExternalServices;

public class PaymentRechargeResponseFaker : IFaker<PaymentRechargeResponse>
{
    public PaymentRechargeResponse GetValid()
        => new PaymentRechargeResponse(
            isPaid: true,
            hash: Guid.NewGuid().ToString());

    public PaymentRechargeResponse GetInvalid()
        => new PaymentRechargeResponse(
            isPaid: false,
            hash: "");
}