using Congreso.Context;
using Congreso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class ParticipanteController : ControllerBase
{
    private readonly CongresoContext _context;

    public ParticipanteController(CongresoContext context)
    {
        _context = context;
    }

    // GET: api/Participante
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Participante>>> GetParticipante()
    {
        return await _context.Participantes.ToListAsync();
    }

    // GET: api/Participante/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Participante>> GetParticipante(int id)
    {
        var participante = await _context.Participantes.FindAsync(id);

        if (participante == null)
        {
            return NotFound();
        }

        return participante;
    }

    // POST: api/Participante
    [HttpPost]
    public async Task<ActionResult<Participante>> PostParticipante(Participante participante)
    {
        _context.Participantes.Add(participante);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetParticipante), new { id = participante.Id }, participante);
    }

    // PUT: api/Participante/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutParticipante(int id, [FromBody] Participante participante)
    {
        if (id != participante.Id)
        {
            return BadRequest();
        }

        // Obtener el participante actual de la base de datos
        var participanteExistente = await _context.Participantes.FindAsync(id);

        if (participanteExistente == null)
        {
            return NotFound();
        }

        // Actualizar solo los campos modificados
        _context.Entry(participanteExistente).CurrentValues.SetValues(participante);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ParticipanteExists(id))
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


    // DELETE: api/Participante/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParticipante(int id)
    {
        var participante = await _context.Participantes.FindAsync(id);
        if (participante == null)
        {
            return NotFound();
        }

        _context.Participantes.Remove(participante);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ParticipanteExists(int id)
    {
        return _context.Participantes.Any(e => e.Id == id);
    }
}
