using StorEsc.Application.Dtos;

namespace StorEsc.API.Token.Interfaces;

public interface ITokenService
{
    Token GenerateCustomerToken(CustomerDto customerDto);
    Token GenerateSellerToken(SellerDto sellerDto);
    Token GenerateAdministratorToken(AdministratorDto administratorDto);
}