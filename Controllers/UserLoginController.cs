using MatutesAuctionHouse.Models;
using MatutesAuctionHouse.Models.Response;
using MatutesAuctionHouse.Services;
using Microsoft.AspNetCore.Mvc;

namespace MatutesAuctionHouse.Controllers
{
    public class UserLoginController : Controller
    {
        private IUserService _userService;

        public UserLoginController(IUserService userService)
        {
            _userService = userService;
        }
        

        //TODO correct message, test the url
        [HttpPost("api/Users/login")]
        public IActionResult Authentication([FromBody] AuthRequest model)
        {
            Response response = new Response();
            var userresponse = _userService.Auth(model);
            if (userresponse == null) 
            {
                response.success = 0;
                response.message = "Email or password wrongs";
                return BadRequest(response);
            }
            response.success = 1;
            response.data = userresponse;
            return Ok(response);
        }
    }
}
