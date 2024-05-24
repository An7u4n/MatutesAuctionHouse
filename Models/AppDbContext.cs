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
                .HasIndex(p => p.email )
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(p => p.user_name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(user => user.Items)
                .WithOne(item => item.User)
                .HasForeignKey(item => item.user_id);

            modelBuilder.Entity<Auction>()
                .HasOne(auction => auction.Item)
                .WithOne(item => item.Auction)
                .HasForeignKey<Auction>(auction => auction.item_id)
                .OnDelete(DeleteBehavior.NoAction);

            /*modelBuilder.Entity<Auction>()
                .HasOne(auction => auction.AuctionPrice)
                .WithOne(price => price.Auction)
                .HasForeignKey<Auction>(price => price.auction_id);*/

            modelBuilder.Entity<AuctionPrice>()
                .HasOne(price => price.User)
                .WithMany(user => user.Bids)
                .HasForeignKey(price => price.user_id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
