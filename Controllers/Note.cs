using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public NotesController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes([FromQuery] string User)
        {
            if (User == null)
                return BadRequest("You have to include the User parameter");
            return await _context.Notes.Where(note => note.User == User).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(long id, [FromQuery] string User)
        {
            if (User == null)
                return BadRequest("You have to include the User parameter");

            var note = await _context.Notes.SingleOrDefaultAsync(note =>
                note.Id == id && note.User == User
            );

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(long id, NoteDTO noteDTO, [FromQuery] string User)
        {
            if (User == null)
                return BadRequest("You have to include the User parameter");

            var existingNote = await _context.Notes.SingleOrDefaultAsync(note =>
                note.Id == id && note.User == User
            );

            if (existingNote == null)
                return NotFound("Couldn't Find Note specified");

            existingNote.UpdatedAt = DateTime.UtcNow;
            existingNote.Title = noteDTO.Title;
            existingNote.HtmlContent = noteDTO.HtmlContent;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(NoteDTO Note, [FromQuery] string User)
        {
            var newNote = new Note()
            {
                HtmlContent = Note.HtmlContent,
                Title = Note.Title,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                User = User
            };
            _context.Notes.Add(newNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = newNote.Id }, newNote);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(long id, [FromQuery] string User)
        {
            if (User == null)
                return BadRequest("You have to include the User parameter");

            var note = await _context.Notes.SingleOrDefaultAsync(note =>
                note.Id == id && note.User == User
            );
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(long id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
