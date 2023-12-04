using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Congreso.Models;
using Congreso.Context;

[Route("api/[controller]")]
[ApiController]
public class ConferenciumController : ControllerBase
{
    private readonly CongresoContext _context;

    public ConferenciumController(CongresoContext context)
    {
        _context = context;
    }

    // GET: api/Conferencium
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Conferencium>>> GetConferencium()
    {
        return await _context.Conferencia.ToListAsync();
    }

    // GET: api/Conferencium/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Conferencium>> GetConferencium(int id)
    {
        var conferencium = await _context.Conferencia.FindAsync(id);

        if (conferencium == null)
        {
            return NotFound();
        }

        return conferencium;
    }

    // POST: api/Conferencium
    [HttpPost]
    public async Task<ActionResult<Conferencium>> PostConferencium(Conferencium conferencium)
    {
        _context.Conferencia.Add(conferencium);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetConferencium), new { id = conferencium.Id }, conferencium);
    }

    // PUT: api/Conferencium/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutConferencium(int id, Conferencium conferencium)
    {
        if (id != conferencium.Id)
        {
            return BadRequest();
        }

        _context.Entry(conferencium).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ConferenciumExists(id))
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

    // DELETE: api/Conferencium/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConferencium(int id)
    {
        var conferencium = await _context.Conferencia.FindAsync(id);
        if (conferencium == null)
        {
            return NotFound();
        }

        _context.Conferencia.Remove(conferencium);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ConferenciumExists(int id)
    {
        return _context.Conferencia.Any(e => e.Id == id);
    }
}
