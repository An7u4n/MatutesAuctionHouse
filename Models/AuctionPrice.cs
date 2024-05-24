using System.ComponentModel.DataAnnotations;

namespace MatutesAuctionHouse.Models
{
    public class AuctionPrice
    {
        [Key] public int? auction_id {  get; set; }
        [Required] public int price { get; set; }
        [Required] public int? user_id { get; set; }
        //public Auction? Auction { get; set; }
        public User? User { get; set; }
    }
}
