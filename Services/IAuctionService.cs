using MatutesAuctionHouse.Models;

namespace MatutesAuctionHouse.Services
{
    public interface IAuctionService
    {
        Task<AuctionPrice> PlaceBidAsync(int auction_id, int user_id, int price);
    }
}
