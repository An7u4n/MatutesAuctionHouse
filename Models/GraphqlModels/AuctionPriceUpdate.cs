namespace MatutesAuctionHouse.Models
{

    public record AuctionPriceInput(
        int auction_id,
        int price,
        int user_id);

    public class AuctionPricePayload
    {
        public AuctionPricePayload(AuctionPrice auctionPrice)
        {
            AuctionPrice = auctionPrice;
        }

        public AuctionPrice AuctionPrice { get; }
    }

    public class Mutation
    {
        public async Task<AuctionPricePayload> AuctionPriceAsync(AuctionPriceInput input, AppDbContext appDbContext)
        {
            var price = new AuctionPrice
            {
                auction_id = input.auction_id,
                price = input.price,
                user_id = input.user_id
            };

            return new AuctionPricePayload(price);
        }
    }

}