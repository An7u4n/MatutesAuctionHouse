using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatutesAuctionHouse.Models
{
    public class Item
    {
        [Required][Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int item_id { get; set; }
        [Required] public string item_name { get; set; }
        [Required] public string item_description { get; set; }
        [ForeignKey("user_id")]
        [Required] public int user_id { get; set; }
        public User? User { get; set; }
        public Auction? Auction { get; set; }
    }
    public class ItemDto
    {
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public int user_id { get; set; }
    }

}
