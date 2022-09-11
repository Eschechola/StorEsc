using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.ExternalServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class PaymentDomainService : IPaymentDomainService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentExternalService _paymentExternalService;
    
    public PaymentDomainService(
        IPaymentRepository paymentRepository,
        IPaymentExternalService paymentExternalService)
    {
        _paymentRepository = paymentRepository;
        _paymentExternalService = paymentExternalService;
    }

    public async Task<Payment> PayRechargeAsync(double amount, CreditCard creditCard)
    {
        Payment payment;
        var paymentResponse = await  _paymentExternalService.PayRechargeAsync(amount, creditCard);

        if (!paymentResponse.IsPaid)
            payment = new Payment(isPaid: false);
        else
            payment = new Payment(
                hash: paymentResponse.Hash,
                isPaid: true);
            
        _paymentRepository.Create(payment);
        await _paymentRepository.UnitOfWork.SaveChangesAsync();
        
        return payment;
    }
}