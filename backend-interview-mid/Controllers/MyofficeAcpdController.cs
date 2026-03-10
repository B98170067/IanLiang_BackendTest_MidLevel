using backend_interview_mid.Contexts;
using backend_interview_mid.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_interview_mid.Controllers
{
    [Route("api/myofficeacpd")]
    [ApiController]
    public class MyofficeAcpdController : ControllerBase
    {
        private readonly BackendExamHubDbContext _context;

        public MyofficeAcpdController(BackendExamHubDbContext context)
        {
            _context = context;
        }

        // GET: api/myofficeacpd
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyOfficeAcpd>>> GetMyOfficeAcpds()
        {
            if (_context.MyOfficeAcpds == null)
            {
                return NotFound();
            }
            return await _context.MyOfficeAcpds.ToListAsync();
        }

        // GET: api/myofficeacpd/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MyOfficeAcpd>> GetMyOfficeAcpd(string id)
        {
            if (_context.MyOfficeAcpds == null)
            {
                return NotFound();
            }
            var myOfficeAcpd = await _context.MyOfficeAcpds.FindAsync(id);

            if (myOfficeAcpd == null)
            {
                return NotFound();
            }

            return myOfficeAcpd;
        }

        // POST: api/myofficeacpd
        [HttpPost]
        public async Task<ActionResult<MyOfficeAcpd>> PostMyOfficeAcpd(MyOfficeAcpd myOfficeAcpd)
        {
            if (_context.MyOfficeAcpds == null)
            {
                return Problem("Entity set 'BackendExamHubDbContext.MyOfficeAcpds' is null.");
            }
            
            myOfficeAcpd.AcpdSid = Guid.NewGuid().ToString("N").Substring(0, 20).ToUpper(); 
            myOfficeAcpd.AcpdNowDateTime = DateTime.Now;
            myOfficeAcpd.AcpdUpddateTime = DateTime.Now;

            _context.MyOfficeAcpds.Add(myOfficeAcpd);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MyOfficeAcpdExists(myOfficeAcpd.AcpdSid))
                {
                    return Conflict(); 
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetMyOfficeAcpd), new { id = myOfficeAcpd.AcpdSid }, myOfficeAcpd);
        }

        // PUT: api/myofficeacpd/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMyOfficeAcpd(string id, MyOfficeAcpd myOfficeAcpd)
        {
            if (id != myOfficeAcpd.AcpdSid)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(myOfficeAcpd.AcpdSid))
            {
                return BadRequest("ACPD_SID cannot be null or empty.");
            }

            myOfficeAcpd.AcpdUpddateTime = DateTime.Now;

            _context.Entry(myOfficeAcpd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyOfficeAcpdExists(id))
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

        // DELETE: api/myofficeacpd/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyOfficeAcpd(string id)
        {
            if (_context.MyOfficeAcpds == null)
            {
                return NotFound();
            }
            var myOfficeAcpd = await _context.MyOfficeAcpds.FindAsync(id);
            if (myOfficeAcpd == null)
            {
                return NotFound();
            }

            _context.MyOfficeAcpds.Remove(myOfficeAcpd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MyOfficeAcpdExists(string id)
        {
            return (_context.MyOfficeAcpds?.Any(e => e.AcpdSid == id)).GetValueOrDefault();
        }
    }
}
