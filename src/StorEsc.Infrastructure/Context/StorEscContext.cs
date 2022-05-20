using Microsoft.EntityFrameworkCore;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Context;

public class StorEscContext : DbContext
{
    #region Constructors

    public StorEscContext()
    {
    }
    
    public StorEscContext(DbContextOptions<StorEscContext> options) : base(options)
    {
    }
    
    #endregion

    #region Properties

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    #endregion
    
    
    
    
}