using Microsoft.EntityFrameworkCore;

namespace MatutesAuctionHouse.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Auction> Auctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Auction>().ToTable("Auction");
            modelBuilder.Entity<Item>()
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.user_id);
            modelBuilder.Entity<Auction>()
                .HasOne(i => i.Item)
                .WithMany()
                .HasForeignKey(i => i.item_id);
        }

    }
}
