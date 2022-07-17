using StorEsc.Domain.Entities;
using StorEsc.Infrastructure.Context;
using StorEsc.Infrastructure.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.Infrastructure.Repositories;

public class WalletRepository : Repository<Wallet>, IWalletRepository
{
    private readonly StorEscContext _context;
    
    public WalletRepository(StorEscContext context)
        : base(context)
    {
        _context = context;
        
    }

    public override IUnitOfWork UnitOfWork => _context;
}