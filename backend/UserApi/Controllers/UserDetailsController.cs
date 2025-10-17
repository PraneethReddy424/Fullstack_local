using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDetailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAll()
        {
            return await _context.UsersDetails.ToListAsync();
        }

        // GET: api/UserDetails/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetById(Guid id)
        {
            var user = await _context.UsersDetails.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");
            return user;
        }

        // POST: api/UserDetails
        [HttpPost]
        public async Task<ActionResult<UserDetails>> Create(UserDetails user)
        {
            user.UserId = Guid.NewGuid(); // assign new UUID
            _context.UsersDetails.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        // PUT: api/UserDetails/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserDetails updatedUser)
        {
            if (id != updatedUser.UserId)
                return BadRequest("ID mismatch.");

            var user = await _context.UsersDetails.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            // Update fields
            user.UserName = updatedUser.UserName;
            user.UserEmail = updatedUser.UserEmail;
            user.ExpirationDate = updatedUser.ExpirationDate;
            user.IsFirstTimeLogging = updatedUser.IsFirstTimeLogging;
            user.IsExternal = updatedUser.IsExternal;
            user.Active = updatedUser.Active;
            user.ModifiedBy = updatedUser.ModifiedBy;
            user.ModifiedOn = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/UserDetails/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _context.UsersDetails.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            _context.UsersDetails.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
