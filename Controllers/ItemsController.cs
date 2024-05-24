using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatutesAuctionHouse.Models;
using Microsoft.AspNetCore.Authorization;

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

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        public async Task<ActionResult<ItemDto>> PostItem(ItemDto itemDto)
        {
          if (_context.Items == null)
          {
              return Problem("Entity set 'AppDbContext.Items'  is null.");
          }

            var user = await _context.Users.FindAsync(itemDto.user_id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var item = new Item
            {
                item_name = itemDto.item_name,
                item_description = itemDto.item_description,
                user_id = itemDto.user_id,
                User = user
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var result = new ItemDto
            {
                item_id = item.item_id,
                item_name = item.item_name,
                item_description = item.item_description,
                user_id = item.user_id
            };

            return CreatedAtAction("GetItem", new { id = item.item_id }, result);
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
