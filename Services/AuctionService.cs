using MatutesAuctionHouse.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace MatutesAuctionHouse.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly AppDbContext _context;

        public AuctionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AuctionPrice> PlaceBidAsync(int auction_id, int user_id, int price)
        {
            var auction = _context.Auctions.Find(auction_id);
            if (auction == null) throw new InvalidOperationException("Auction doesnt exists");
            AuctionPrice? auctionPrice = await _context.AuctionPrices.AsNoTracking().FirstOrDefaultAsync(auc => auc.auction_id == auction_id);
            DateTime auction_end_date = auction.auction_start_date.AddHours(2);
            if (DateTime.Now < auction.auction_start_date || DateTime.Now > auction_end_date || auctionPrice == null)
            {
                throw new InvalidOperationException("Invalid auction or auction has ended. "+DateTime.Now+" "+auction.auction_start_date+" "+auction_end_date);
            }

            auctionPrice.user_id = user_id;
            auctionPrice.price = price;

            // Anexar la instancia modificada al contexto y marcarla como modificada
            _context.AuctionPrices.Attach(auctionPrice);
            _context.Entry(auctionPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return auctionPrice;
        }
    }
}
