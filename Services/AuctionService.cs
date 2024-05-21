using MatutesAuctionHouse.Models;

namespace MatutesAuctionHouse.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly AppDbContext _context;

        public AuctionService(AppDbContext context)
        {
            _context = context;
        }

        public Auction GetAuctionById(int id) 
        {
            Auction ret = _context.Auctions.FirstOrDefault(x => x.auction_id == id);

            if(ret  == null) 
                throw new ArgumentException("The auction ID doesnt exist", nameof(id));

            return ret;
        }
    }
}
