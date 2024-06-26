﻿using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatutesAuctionHouse.Models
{
    public class User
    {
        [Required][Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int user_id { get; set; }
        [Required] public string user_name { get; set; }
        [Required] public string email {  get; set; }
        [Required] public string password {  get; set; }
        public byte[]? profile_image { get; set; }

        public ICollection<Item> Items { get; } = new List<Item>();
        public ICollection<AuctionPrice> Bids { get; } = new List<AuctionPrice>();
    }
}
