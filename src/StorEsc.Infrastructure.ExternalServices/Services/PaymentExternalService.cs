using StorEsc.Domain.Entities;
using StorEsc.Infrastructure.ExternalServices.Interfaces;
using StorEsc.Infrastructure.ExternalServices.Responses;

namespace StorEsc.Infrastructure.ExternalServices.Services;

public class PaymentExternalService : IPaymentExternalService
{
    public async Task<PaymentRechargeResponse> PayRechargeAsync(
        double amount,
        CreditCard creditCard)
    {
        // Implement payment gateway to made recharge payment

        var isPaid = new Random().Next(100) > 20;

        if (isPaid)
            return new PaymentRechargeResponse(
                hash: Guid.NewGuid().ToString(),
                isPaid: true);

        return new PaymentRechargeResponse(isPaid: false);
    }
}