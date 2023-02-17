using StorEsc.Application.DTOs;

namespace StorEsc.API.Token.Interfaces;

public interface ITokenService
{
    Token GenerateCustomerToken(CustomerDTO customerDTO);
    Token GenerateSellerToken(SellerDTO sellerDTO);
    Token GenerateAdministratorToken(AdministratorDTO administratorDTO);
}