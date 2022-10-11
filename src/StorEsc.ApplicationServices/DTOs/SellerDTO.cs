namespace StorEsc.Application.DTOs;

public class SellerDTO : AccountDTO
{
    public Guid WalletId { get; set; }
    public WalletDTO Wallet { get; set; }
    public IList<ProductDTO> Products { get; set; }

    public SellerDTO()
    {
        Products = new List<ProductDTO>();
    }
}