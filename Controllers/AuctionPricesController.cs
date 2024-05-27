using MatutesAuctionHouse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatutesAuctionHouse.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuctionPricesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuctionPricesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionPrice>> GetAuction(int id)
        {
            if (_context.Auctions == null)
            {
                return NotFound();
            }
            var auctionPrice = await _context.AuctionPrices.FirstOrDefaultAsync(price => price.auction_id == id);

            if (auctionPrice == null)
            {
                return NotFound();
            }

            return auctionPrice;
        }
    }
}
