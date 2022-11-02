using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL;

public class BuyMyHouseContext : DbContext
{
    public BuyMyHouseContext(DbContextOptions<BuyMyHouseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mortgage>()
            .HasOne(x => x.User)
            .WithOne(y => y.Mortgage);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<House> Houses { get; set; }
    public DbSet<Mortgage> Mortgages { get; set; }
}