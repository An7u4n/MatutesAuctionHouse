using MatutesAuctionHouse.Models;
using MatutesAuctionHouse.Models.Response;
using Microsoft.Identity.Client;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatutesAuctionHouse.Tools;

namespace MatutesAuctionHouse.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userResponse = new UserResponse();
            string spassword = Encrypt.GetSHA256(model.password);
            var user = _context.Users.Where(d => d.email == model.email && d.password == spassword).FirstOrDefault();
            if (user != null) userResponse.email = user.email;
            return userResponse;
        }
    }
}
