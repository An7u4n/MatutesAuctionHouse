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
        public DbSet<AuctionPrice> AuctionPrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Auction>().ToTable("Auction");
            modelBuilder.Entity<AuctionPrice>().ToTable("AuctionPrice");

            modelBuilder.Entity<User>()
                .HasIndex(p => new { p.user_name, p.email })
                .IsUnique();

            modelBuilder.Entity<Item>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(i => i.user_id);

            modelBuilder.Entity<Item>()
                .HasOne(navAuct => navAuct.Auction)
                .WithOne(navItem => navItem.Item)
                .HasForeignKey<Auction>(key => key.item_id);

            modelBuilder.Entity<Auction>()
                .HasOne(a => a.AuctionPrice)
                .WithOne(ap => ap.Auction)
                .HasForeignKey<AuctionPrice>(ap => ap.auction_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AuctionPrice>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(i => i.user_id);
        }
    }
}
