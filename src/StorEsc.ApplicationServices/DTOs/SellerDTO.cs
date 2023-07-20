namespace StorEsc.Application.Dtos;

public class SellerDto : AccountDto
{
    public Guid WalletId { get; set; }
    public WalletDto Wallet { get; set; }
    public IList<ProductDto> Products { get; set; }

    public SellerDto()
    {
        Products = new List<ProductDto>();
    }
}