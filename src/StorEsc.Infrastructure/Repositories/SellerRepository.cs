using StorEsc.Domain.Entities;
using StorEsc.Infrastructure.Context;
using StorEsc.Infrastructure.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.Infrastructure.Repositories;

public class SellerRepository : Repository<Seller>, ISellerRepository
{
    private readonly StorEscContext _context;

    public SellerRepository(StorEscContext context) : base(context)
    {
        _context = context;
    }

    public override IUnitOfWork UnitOfWork => _context;

    public async Task<bool> ExistsByEmailAsync(string email)
        => await ExistsAsync(entity => entity.Email.ToLower() == email.ToLower());

    public async Task<Seller> GetByEmailAsync(string email, string includeProperties = "")
        => await GetAsync(entity => 
            entity.Email.ToLower() == email.ToLower(),
            includeProperties);
}