using Common.Domain;
using Common.Domain.ValueObjects;
using Common.Domain.ValueObjects.Money;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Domain.Category.Entities;
using Shop.Domain.Comment.Entities;
using Shop.Domain.Order.Entities;
using Shop.Domain.Product.Entities;
using Shop.Domain.Role.Entities;
using Shop.Domain.Seller.Entities;
using Shop.Domain.User.Entites;
using Shop.Domain.WebSiteEntities.Entities;
using Shop.Infrastructure._Utilities.MediatR;

namespace Shop.Infrastructure.Persistent.EF.Context;

public class BookStoreContext : DbContext
{
    private readonly ICustomPublisher _publisher;
    public BookStoreContext(DbContextOptions<BookStoreContext> options, ICustomPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<Domain.Category.Entities.Category> Categories { get; set; }
    public DbSet<Domain.Comment.Entities.Comment> Comments { get; set; }
    public DbSet<Domain.Order.Entities.Order> Orders { get; set; }
    public DbSet<Domain.Product.Entities.Product> Products { get; set; }
    public DbSet<Domain.Role.Entities.Role> Roles { get; set; }
    public DbSet<Domain.Seller.Entities.Seller> Sellers { get; set; }
    public DbSet<SellerInventory> Inventories { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Domain.User.Entites.User> Users { get; set; }
    public DbSet<ShippingMethod> ShippingMethods { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var modifiedEntities = GetModifiedEntities();
        await PublishEvents(modifiedEntities);
        return await base.SaveChangesAsync(cancellationToken);
    }
    private List<AggregateRoot> GetModifiedEntities() =>
        ChangeTracker.Entries<AggregateRoot>()
            .Where(x => x.State != EntityState.Detached)
            .Select(c => c.Entity)
            .Where(c => c.DomainEvents.Any()).ToList();

    private async Task PublishEvents(List<AggregateRoot> modifiedEntities)
    {
        foreach (var entity in modifiedEntities)
        {
            var events = entity.DomainEvents;
            foreach (var domainEvent in events)
            {
                await _publisher.Publish(domainEvent, PublishStrategy.ParallelNoWait);
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
       
        builder.Properties<Toman>()
            .HaveConversion<TomanConverter>()
            .HavePrecision(18, 0)
            .HaveColumnType("decimal(18,0)");

        builder.Properties<PhoneNumber>()
            .HaveConversion<PhoneNumberConverter>()
            .HaveMaxLength(11);
    }
}