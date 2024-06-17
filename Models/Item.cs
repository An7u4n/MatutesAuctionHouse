using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatutesAuctionHouse.Models
{
    public class Item
    {
        [Required][Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int item_id { get; set; }
        [Required] public string item_name { get; set; }
        [Required] public string item_description { get; set; }
        [Required] public int user_id { get; set; }
        [ForeignKey("user_id")]
        public virtual User User { get; set; }
        public byte[]? itemImage { get; set; }
        public virtual ICollection<Auction> Auction { get; set; }
    }
    public class ItemDto
    {
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public int user_id { get; set; }
        public byte[]? itemImage { get; set; }
    }

}
