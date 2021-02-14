using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using KO.Core;
using KO.Data;

namespace KBS.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeController : ControllerBase
    {
        private readonly KnowledgeDbContext _context;
        private readonly UserManager<KoUser> _userManager;

        public KnowledgeController(KnowledgeDbContext context, UserManager<KoUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Knowledge
        [HttpGet]
        public  async Task<IEnumerable<FieldText>> GetFieldText()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

           var records= _context.FieldText.Where(x => x.User.Id == currentUser.Id).AsEnumerable();

            return records;
            
        }

        // GET: api/Knowledge/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFieldText([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var FieldText = await _context.FieldText.FindAsync(id);

            if (FieldText == null)
            {
                return NotFound();
            }

            return Ok(FieldText);
        }

        // PUT: api/Knowledge/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFieldText([FromRoute] int id, [FromBody] FieldText FieldText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != FieldText.Id)
            {
                return BadRequest();
            }

            _context.Entry(FieldText).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldTextExists(id))
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

        // POST: api/Knowledge
        [HttpPost]
        public async Task<IActionResult> PostFieldText([FromBody] FieldText FieldText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FieldText.Add(FieldText);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFieldText", new { id = FieldText.Id }, FieldText);
        }

        // DELETE: api/Knowledge/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFieldText([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var FieldText = await _context.FieldText.FindAsync(id);
            if (FieldText == null)
            {
                return NotFound();
            }

            _context.FieldText.Remove(FieldText);
            await _context.SaveChangesAsync();

            return Ok(FieldText);
        }

        private bool FieldTextExists(int id)
        {
            return _context.FieldText.Any(e => e.Id == id);
        }
    }
}