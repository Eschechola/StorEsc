namespace StorEsc.Application.DTOs;

public class SellerDTO : AccountDTO
{
    public IList<ProductDTO> Products { get; private set; }

    public SellerDTO()
    {
        Products = new List<ProductDTO>();
    }
}