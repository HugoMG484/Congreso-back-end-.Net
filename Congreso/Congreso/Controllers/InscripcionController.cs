using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Congreso.Models;
using Congreso.Context;

[Route("api/[controller]")]
[ApiController]
public class InscripcionController : ControllerBase
{
    private readonly CongresoContext _context;

    public InscripcionController(CongresoContext context)
    {
        _context = context;
    }

    // GET: api/Inscripcion
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inscripcion>>> GetInscripcion()
    {
        return await _context.Inscripcions.ToListAsync();
    }

    // GET: api/Inscripcion/conferenciaId/3
    [HttpGet("ConferenciaId/{conferenciaId}")]
    public async Task<ActionResult<IEnumerable<Inscripcion>>> GetConferenciaId(int conferenciaId)
    {
        return await _context.Inscripcions
            .Where(i => i.ConfirmacionAsistencia && i.ConferenciaId == conferenciaId)
            .ToListAsync();
    }

    // GET: api/Inscripcion/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Inscripcion>> GetInscripcion(int id)
    {
        var inscripcion = await _context.Inscripcions.FindAsync(id);

        if (inscripcion == null)
        {
            return NotFound();
        }

        return inscripcion;
    }

    // POST: api/Inscripcion
    [HttpPost]
    public async Task<ActionResult<Inscripcion>> PostInscripcion(Inscripcion inscripcion)
    {
        try
        {
            _context.Inscripcions.Add(inscripcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInscripcion), new { id = inscripcion.Id }, inscripcion);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // PUT: api/Inscripcion/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutInscripcion(int id, Inscripcion inscripcion)
    {
        if (id != inscripcion.Id)
        {
            return BadRequest();
        }

        _context.Entry(inscripcion).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InscripcionExists(id))
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

    // DELETE: api/Inscripcion/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInscripcion(int id)
    {
        var inscripcion = await _context.Inscripcions.FindAsync(id);
        if (inscripcion == null)
        {
            return NotFound();
        }

        _context.Inscripcions.Remove(inscripcion);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool InscripcionExists(int id)
    {
        return _context.Inscripcions.Any(e => e.Id == id);
    }
}
