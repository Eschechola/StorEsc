using StorEsc.Domain.Entities;
using StorEsc.Infrastructure.Context;
using StorEsc.Infrastructure.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.Infrastructure.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly StorEscContext _context;

    public CustomerRepository(StorEscContext context) : base(context)
    {
        _context = context;
    }

    public override IUnitOfWork UnitOfWork => _context;
    
    public async Task<Customer> GetByEmailAsync(string email, string includeProperties = "")
        => await GetAsync(
            entity => entity.Email.ToLower() == email.ToLower(),
            includeProperties);

    public async Task<bool> ExistsByEmailAsync(string email)
        => await ExistsAsync(entity => entity.Email.ToLower() == email.ToLower());
}