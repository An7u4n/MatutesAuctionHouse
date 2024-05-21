using System.ComponentModel.DataAnnotations;

namespace MatutesAuctionHouse.Models
{
    public class AuctionPrice
    {
        [Key] public int auction_id {  get; set; }
        [Required] public int price { get; set; }
    }
}
