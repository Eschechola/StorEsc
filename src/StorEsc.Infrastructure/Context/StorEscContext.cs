using Microsoft.EntityFrameworkCore;
using StorEsc.Domain.Entities;
using StorEsc.Infrastructure.Interfaces;
using StorEsc.Infrastructure.Maps;

namespace StorEsc.Infrastructure.Context;

public class StorEscContext : DbContext, IUnitOfWork
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
    public DbSet<Wallet> Recharges { get; set; }

    #endregion
    
    #region Maps

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CustomerMap());
        builder.ApplyConfiguration(new OrderMap());
        builder.ApplyConfiguration(new OrderItemMap());
        builder.ApplyConfiguration(new ProductMap());
        builder.ApplyConfiguration(new SellerMap());
        builder.ApplyConfiguration(new WalletMap());
        builder.ApplyConfiguration(new RechargeMap());
        builder.ApplyConfiguration(new VoucherMap());
    }

    #endregion
    
    #region Unit of Work Methods

    public void BeginTransaction()
        => this.Database.BeginTransaction();

    public void Commit()
        => this.Database.CommitTransaction();

    public void Rollback()
        => this.Database.RollbackTransaction();

    public async Task BeginTransactionAsync()
        => await this.Database.BeginTransactionAsync();

    public async Task CommitAsync()
        => await this.Database.CommitTransactionAsync();

    public async Task RollbackAsync()
        => await this.Database.RollbackTransactionAsync();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        FormatEntity();
        return await base.SaveChangesAsync();
    }

    #endregion

    #region Class Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    private void FormatEntity()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => 
                x.Entity is Entity && 
                (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
                (entity.Entity as Entity).CreatedAtNow();

            (entity.Entity as Entity).UpdatedAtNow();
        }
    }

    #endregion
}