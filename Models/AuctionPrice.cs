﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatutesAuctionHouse.Models
{
    public class AuctionPrice
    {
        [Key] public int auction_price_id {  get; set; }
        [Required] public int auction_id { get; set; }
        [Required] public int price { get; set; }
        [Required] public int user_id { get; set; }
        [ForeignKey("auction_id")] public virtual Auction Auction { get; set; }
        [ForeignKey("user_id")] public virtual User User { get; set; }
    }
}
