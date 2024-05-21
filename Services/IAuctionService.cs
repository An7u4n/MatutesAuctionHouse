using MatutesAuctionHouse.Models;

namespace MatutesAuctionHouse.Services
{
    public interface IAuctionService
    {
        Auction GetAuctionById(int id);
    }
}
