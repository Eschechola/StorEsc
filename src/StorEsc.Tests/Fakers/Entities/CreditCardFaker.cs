using Bogus;
using Bogus.DataSets;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class CreditCardFaker : BaseFaker<CreditCard>
{
    private readonly Person _person = new();
    private readonly Finance _finance = new();

    public override CreditCard GetValid()
        => new CreditCard(
            holdName: _person.FullName,
            number: _finance.CreditCardNumber(),
            expirationDate: "00/00",
            cvv: int.Parse(_finance.CreditCardCvv()),
            document: "00000000000");

    public override CreditCard GetInvalid()
        => new CreditCard(
            holdName: "",
            number: "",
            expirationDate: "00/00",
            cvv: 0,
            document: "");
}