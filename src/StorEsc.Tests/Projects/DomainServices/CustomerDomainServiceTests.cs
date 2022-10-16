using EscNet.Hashers.Interfaces.Algorithms;
using Moq;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.DomainServices.Interfaces;
using StorEsc.DomainServices.Services;
using StorEsc.Infrastructure.Interfaces.Repositories;
using Xunit;

namespace StorEsc.Tests.Projects.DomainServices;

public class CustomerDomainServiceTests
{
    private readonly ICustomerDomainService _sut;

    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IArgon2IdHasher> _argon2IdHasherMock;
    private readonly Mock<IWalletDomainService> _walletDomainServiceMock;
    private readonly Mock<IDomainNotificationFacade> _domainNotificationMock;

    public CustomerDomainServiceTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _argon2IdHasherMock = new Mock<IArgon2IdHasher>();
        _walletDomainServiceMock = new Mock<IWalletDomainService>();
        _domainNotificationMock = new Mock<IDomainNotificationFacade>();

        _sut = new CustomerDomainService(
            customerRepository: _customerRepositoryMock.Object,
            argon2IdHasher: _argon2IdHasherMock.Object,
            walletDomainService: _walletDomainServiceMock.Object,
            domainNotification: _domainNotificationMock.Object);
    }

    [Fact(DisplayName = "GetCustomerAsync when customer found returns customer")]
    [Trait("CustomerDomainService", "GetCustomerAsync")]
    public async Task GetCustomerAsync_WhenCustomerFound_ReturnsCustomer()
    {
        
    }
}