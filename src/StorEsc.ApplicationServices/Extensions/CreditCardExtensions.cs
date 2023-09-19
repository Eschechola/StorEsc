using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.Application.Extensions;

public static class CreditCardExtensions
{
    public static CreditCardDto AsDto(this CreditCard creditCard)
        => new CreditCardDto
        {
            HoldName = creditCard.HoldName,
            ExpirationDate = creditCard.ExpirationDate,
            Cvv = creditCard.Cvv,
            Document = creditCard.Document,
            Number = creditCard.Number
        };
    
    public static CreditCard AsEntity(this CreditCardDto creditCardDto)
        => new CreditCard(
            holdName: creditCardDto.HoldName,
            number: creditCardDto.Number,
            expirationDate: creditCardDto.ExpirationDate,
            cvv: creditCardDto.Cvv,
            document: creditCardDto.Document
        );

    public static IList<CreditCard> AsEntityList(this IList<CreditCardDto> creditCardDtos)
        => creditCardDtos.Select(creditCard => creditCard.AsEntity()).ToList();
    
    public static IList<CreditCardDto> AsDtoList(this IList<CreditCard> creditCardDtos)
        => creditCardDtos.Select(creditCard => creditCard.AsDto()).ToList();
}