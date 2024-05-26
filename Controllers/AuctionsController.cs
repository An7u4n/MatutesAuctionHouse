using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatutesAuctionHouse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using MatutesAuctionHouse.Hubs;
using MatutesAuctionHouse.Services;
using System.Security.Cryptography;

namespace MatutesAuctionHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuctionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAuctionService _auctionService;
        private readonly IHubContext<AuctionHub> _hubContext;

        public AuctionsController(AppDbContext context, IAuctionService auctionService, IHubContext<AuctionHub> hubContext)
        {
            _context = context;
            _auctionService = auctionService;
            _hubContext = hubContext;
        }

        // GET: api/Auctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctions()
        {
          if (_context.Auctions == null)
          {
              return NotFound();
          }
            return await _context.Auctions.ToListAsync();
        }

        // GET: api/Auctions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auction>> GetAuction(int id)
        {
          if (_context.Auctions == null)
          {
              return NotFound();
          }
            var auction = await _context.Auctions.FindAsync(id);

            if (auction == null)
            {
                return NotFound();
            }

            return auction;
        }

        // PUT: api/Auctions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuction(int id, Auction auction)
        {
            if (id != auction.auction_id)
            {
                return BadRequest();
            }

            _context.Entry(auction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Auctions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuctionDto>> PostAuction(AuctionDto auctionDto)
        {
            if (_context.Auctions == null)
            {
                return Problem("Entity set 'AppDbContext.Auctions'  is null.");
            }

            if (auctionDto.auction_start_date < DateTime.Now) return BadRequest("Auction start time is past.");

            // Check if there is an auction for the sended item
            var exist = await _context.Auctions.AnyAsync(a => a.item_id == auctionDto.item_id);

            if (exist) return BadRequest("There is an auction for this item");

            // Validar existencia del item
            var item = await _context.Items.FindAsync(auctionDto.item_id);

            if (item == null) return NotFound("Item not found.");

            var auction = new Auction
            {
                auction_start_date = auctionDto.auction_start_date,
                item_id = auctionDto.item_id,
                Item = item
            };

            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();

            // Create an AuctionPrice for the Auction

            var user = await _context.Users.FindAsync(item.user_id);
            if (user == null) return NotFound("The item has no owner");

            var auctionPrice = new AuctionPrice
            {
                auction_id = auction.auction_id,
                price = 0,
                user_id = user.user_id,
                User = user,
                Auction = auction
            };

            _context.AuctionPrices.Add(auctionPrice);
            await _context.SaveChangesAsync();

            var result = new AuctionDto
            {
                auction_id = auction.auction_id,
                auction_start_date = auction.auction_start_date,
                item_id = auction.item_id
            };

            return CreatedAtAction("GetAuction", new { id = auction.auction_id }, result);
        }

        // DELETE: api/Auctions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            if (_context.Auctions == null)
            {
                return NotFound();
            }
            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            _context.Auctions.Remove(auction);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/AuctionPrice")]
        public async Task<IActionResult> PlaceBid(int id, [FromBody] BidRequest bidRequest)
        {
            try
            {
                var auctionPrice = await _auctionService.PlaceBidAsync(id, bidRequest.user_id, bidRequest.price);
                await _hubContext.Clients.All.SendAsync("ReceiveBidUpdate", id, bidRequest.price);
                return Ok(auctionPrice);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool AuctionExists(int id)
        {
            return (_context.Auctions?.Any(e => e.auction_id == id)).GetValueOrDefault();
        }
        public class BidRequest
        {
            public int user_id { get; set; }
            public int price { get; set; }
        }
    }
}
