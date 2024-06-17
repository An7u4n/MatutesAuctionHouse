using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatutesAuctionHouse.Models;
using Microsoft.AspNetCore.Authorization;
using MatutesAuctionHouse.Models.Response;

namespace MatutesAuctionHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            return await _context.Items.ToListAsync();
        }
        // Get item image
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetItemImage(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null || item.itemImage == null) return NotFound("Image not found.");

            byte[] imageBytes = item.itemImage;

            return File(imageBytes, "image/jpeg");
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        // GET Items not sold by user
        [HttpGet("notsold/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByOwner(int id)
        {
            if (_context.Items == null)
            {
                return NotFound();
            }
            return await _context.Items.Where(i => i.user_id == id).ToListAsync();
        }


        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.item_id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem([FromForm] string item_name, [FromForm] string item_description, [FromForm] int user_id, [FromForm] IFormFile image)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'AppDbContext.Items'  is null.");
            }

            var user = await _context.Users.FindAsync(user_id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            if (image == null || image.Length == 0)
            {
                return BadRequest(new { Message = "Image is required" });
            }

            var item = new Item
            {
                item_name = item_name,
                item_description = item_description,
                user_id = user_id,
                User = user,
            };

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                item.itemImage = memoryStream.ToArray();
            }

            user.Items.Add(item);
            _context.Items.Add(item);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.item_id }, new Response { success = 1, message = "Item Added", data = item_name });
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (_context.Items == null)
            {
                return NotFound();
            }
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return (_context.Items?.Any(e => e.item_id == id)).GetValueOrDefault();
        }
    }
}
