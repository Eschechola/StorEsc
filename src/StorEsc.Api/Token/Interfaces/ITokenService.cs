using StorEsc.Application.DTOs;

namespace StorEsc.Api.Token.Interfaces;

public interface ITokenService
{
    Token GenerateCustomerToken(CustomerDTO customerDTO);
    Token GenerateSellerToken(SellerDTO sellerDTO);
}