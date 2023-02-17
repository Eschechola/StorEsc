using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Interfaces.Repositories;

public interface IAdministratorRepository : IRepository<Administrator>
{
    Task<Administrator> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
}