using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StorEsc.API.Token.Interfaces;
using StorEsc.Application.Dtos;

namespace StorEsc.API.Token.Services;

public class TokenService : ITokenService
{
    private readonly string _issuer;
    private readonly string _secretKey;
    private readonly int _hoursToExpire;
    private readonly DateTime _now;
    
    public TokenService(IConfiguration configuration)
    {
        _issuer = configuration["Token:Issuer"];
        _secretKey = configuration["Token:SecretKey"];
        _hoursToExpire = int.Parse(configuration["Token:HoursToExpire"]);
        _now = DateTime.UtcNow.AddHours(-3);
    }
    
    public Token GenerateCustomerToken(CustomerDto customerDto)
    {
        var claims = CreateIdentityClaims(
            uuid: customerDto.Id.ToString(),
            email: customerDto.Email,
            tokenType: TokenType.Customer);
        
        return new Token(
            value: CreateToken(claims),
            issuer: _issuer,
            issuedAt: _now,
            hoursToExpire: _hoursToExpire);
    }

    public Token GenerateSellerToken(SellerDto sellerDto)
    {
        var claims = CreateIdentityClaims(
            uuid: sellerDto.Id.ToString(),
            email: sellerDto.Email,
            tokenType: TokenType.Seller);

        return new Token(
            value: CreateToken(claims),
            issuer: _issuer,
            issuedAt: _now,
            hoursToExpire: _hoursToExpire);
    }

    public Token GenerateAdministratorToken(AdministratorDto administratorDto)
    {
        var claims = CreateIdentityClaims(
            uuid: administratorDto.Id.ToString(),
            email: administratorDto.Email,
            tokenType: TokenType.Administrator);
        
        return new Token(
            value: CreateToken(claims),
            issuer: _issuer,
            issuedAt: _now,
            hoursToExpire: _hoursToExpire);
    }

    private Collection<Claim> CreateIdentityClaims(string uuid, string email, TokenType tokenType)
    {
        var claims = new Collection<Claim>();

        claims.Add(new Claim(ClaimTypes.NameIdentifier, uuid));
        claims.Add(new Claim(ClaimTypes.Email, email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, _now.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, _now.ToString()));
        claims.Add(new Claim(ClaimTypes.Role, GetRoleByTokenType(tokenType)));

        return claims;
    }
    
    private string GetRoleByTokenType(TokenType type)
    {
        return type switch
        {
            TokenType.Customer
                => Roles.Customer,

            TokenType.Seller
                => Roles.Seller,
            
            TokenType.Administrator
                => Roles.Administrator,

            _ => Roles.Customer
        };
    }
    
    private string CreateToken(Collection<Claim> claims)
    {
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_hoursToExpire),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}