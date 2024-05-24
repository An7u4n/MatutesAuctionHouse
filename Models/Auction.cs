using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatutesAuctionHouse.Models
{
    public class Auction
    {
        [Required][Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int auction_id { get; set; }
        [ForeignKey("item_id")]
        [Required] public int item_id { get; set; }
        [Required] public DateTime auction_start_date { get; set; }
        //public AuctionPrice? AuctionPrice { get; set; }
        public Item? Item { get; set; }
    }

    public class AuctionDto
    {
        public int auction_id { get; set; }
        public int item_id { get; set; }
        public DateTime auction_start_date { get; set; }
    }

}
