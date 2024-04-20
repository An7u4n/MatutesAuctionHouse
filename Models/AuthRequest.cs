using System.ComponentModel.DataAnnotations;

namespace MatutesAuctionHouse.Models
{
    public class AuthRequest
    {
        [Required] public string email { get; set; }
        [Required] public string password { get; set; }
    }
}
