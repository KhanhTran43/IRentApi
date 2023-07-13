using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.Model.Entity.Contract;

namespace iRentApi.Controllers
{
    public class EntityController<TEntity, TKey> : ControllerBase where TEntity : EntityBase<TKey>
    {
        private readonly IRentContext _context;

        public EntityController(IRentContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetUsers()
        {
          if (_context.Set<TEntity>() == null)
          {
              return NotFound();
          }
            return await _context.Set<TEntity>().ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> GetUser(long id)
        {
          if (_context.Set<TEntity>() == null)
          {
              return NotFound();
          }
            var user = await _context.Set<TEntity>().FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(TKey id, TEntity user)
        {
            if (!id.Equals(user.Id))
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TEntity>> PostUser(TEntity user)
        {
          if (_context.Set<TEntity>() == null)
          {
              return Problem("Entity set 'IRentContext.Users'  is null.");
          }
            _context.Set<TEntity>().Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.Set<TEntity>() == null)
            {
                return NotFound();
            }
            var user = await _context.Set<TEntity>().FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Set<TEntity>().Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(TKey id)
        {
            return (_context.Set<TEntity>()?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }
    }
}
