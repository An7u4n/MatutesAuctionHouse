using Microsoft.AspNetCore.SignalR;

namespace MatutesAuctionHouse.Hubs
{
    public class AuctionHub : Hub
    {
        public async Task SendBidUpdate(int auctionId, int newPrice)
        {
            await Clients.All.SendAsync("ReceiveBidUpdate", auctionId, newPrice);
        }
    }
}
