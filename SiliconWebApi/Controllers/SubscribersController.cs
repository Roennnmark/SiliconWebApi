using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace SiliconWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpPost]
        public async Task<IActionResult> Create(Subscriber dto)
        {
            if (ModelState.IsValid)
            {
                if (!await _context.Subscribers.AnyAsync(x => x.Email == dto.Email))
                {
                    try
                    {
                        _context.Subscribers.Add(dto);
                        await _context.SaveChangesAsync();
                        return Created("", null);
                    }
                    catch
                    {
                        return Problem("Unable to create subscription");
                    }
                }

                return Conflict("Your email address is already subscribed");
            }

            return BadRequest();
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Email == email);
            if (subscriber != null)
            {
                _context.Subscribers.Remove(subscriber);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }
    }
}
