using MatutesAuctionHouse.Models;
using MatutesAuctionHouse.Models.Response;

namespace MatutesAuctionHouse.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
