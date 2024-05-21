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
    public class Mutation
    {
        private readonly ITopicEventSender _eventSender;

        public Mutation(ITopicEventSender eventSender)
        {
            _eventSender = eventSender;
        }

        public async Task<int> UpdatePrice(int id, int newPrice)
        {
            var priceChange = new AuctionPrice { auction_id = id, price = newPrice };
            await _eventSender.SendAsync(nameof(Subscription.OnPriceChange), priceChange);
            return newPrice;
        }
    }

}
