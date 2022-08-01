namespace StorEsc.Application.DTOs;

public class SellerDTO : AccountDTO
{
    public IList<ProductDTO> Products { get; set; }

    public SellerDTO()
    {
        Products = new List<ProductDTO>();
    }
}