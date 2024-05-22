using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using MatutesAuctionHouse.Services;
using Microsoft.EntityFrameworkCore;

namespace MatutesAuctionHouse.Models
{

    public class Query
    {
        public Auction GetAuctionById(int id, [Service] AuctionService auctionService)
        {
            return auctionService.GetAuctionById(id);
        }
    }

    public class Subscription
    {
        [Subscribe]
        [Topic]
        public AuctionPrice OnPriceChange([EventMessage] AuctionPrice newPrice) => newPrice;
    }
}
