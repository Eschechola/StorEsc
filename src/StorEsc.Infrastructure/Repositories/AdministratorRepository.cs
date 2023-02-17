using StorEsc.Domain.Entities;
using StorEsc.Infrastructure.Context;
using StorEsc.Infrastructure.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.Infrastructure.Repositories;

public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
{
    private readonly StorEscContext _context;

    public AdministratorRepository(StorEscContext context) : base(context)
    {
        _context = context;
    }

    public override IUnitOfWork UnitOfWork => _context;
    
    public async Task<Administrator> GetByEmailAsync(string email)
        => await GetAsync(entity => string.Equals(entity.Email.ToLower(), email.ToLower()));
    
    public async Task<bool> ExistsByEmailAsync(string email)
        => await ExistsAsync(entity => string.Equals(entity.Email.ToLower(), email.ToLower()));
}