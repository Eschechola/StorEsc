using StorEsc.Infrastructure.ExternalServices.Responses;

namespace StorEsc.Tests.Fakers.Infrastructure.ExternalServices;

public class PaymentRechargeResponseFaker : BaseFaker<PaymentRechargeResponse>
{
    public override PaymentRechargeResponse GetValid()
        => new PaymentRechargeResponse(
            isPaid: true,
            hash: Guid.NewGuid().ToString());

    public override PaymentRechargeResponse GetInvalid()
        => new PaymentRechargeResponse(
            isPaid: false,
            hash: "");
}