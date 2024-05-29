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

        public async Task<AuctionPrice> PlaceBidAsync(int auctionId, int userId, int bidPrice)
        {
            var auction = await _context.Auctions
                .Include(a => a.AuctionPrice)
                .FirstOrDefaultAsync(a => a.auction_id == auctionId);

            if (auction == null)
                throw new InvalidOperationException("Auction does not exist.");

            var auctionPrice = auction.AuctionPrice;
            DateTime auctionEndDate = auction.auction_start_date.AddHours(2);

            if (DateTime.Now < auction.auction_start_date || DateTime.Now > auctionEndDate)
                throw new InvalidOperationException($"Auction is not active. Current time: {DateTime.Now}, Auction start: {auction.auction_start_date}, Auction end: {auctionEndDate}");

            if (auctionPrice == null)
                throw new InvalidOperationException("Auction price does not exist.");

            if (auctionPrice.price >= bidPrice)
                throw new InvalidOperationException("The bid amount is lower than or equal to the current highest bid.");

            auctionPrice.user_id = userId;
            auctionPrice.price = bidPrice;

            _context.AuctionPrices.Update(auctionPrice);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("An error occurred while updating the auction price. Please try again.");
            }

            return auctionPrice;
        }
    }
}
