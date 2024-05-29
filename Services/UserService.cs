using MatutesAuctionHouse.Models;
using MatutesAuctionHouse.Models.Response;
using Microsoft.Identity.Client;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatutesAuctionHouse.Tools;
using MatutesAuctionHouse.Models.Common;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MatutesAuctionHouse.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, AppDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userResponse = new UserResponse();
            string spassword = Encrypt.GetSHA256(model.password);
            var user = _context.Users.Where(d => d.email == model.email && d.password == spassword).FirstOrDefault();
            if (user == null) return null;
            userResponse.user_id = user.user_id;
            userResponse.user_name = user.user_name;
            userResponse.email = user.email;
            userResponse.token = GetToken(user);
            return userResponse;
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
                    new Claim(ClaimTypes.Email, user.email),
                }
                ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
