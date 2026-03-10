using backend_interview_mid.Entities;
using backend_interview_mid.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_interview_mid.Controllers
{
    [Route("api/myofficeacpd")]
    [ApiController]
    public class MyofficeAcpdController : ControllerBase
    {
        private readonly IMyofficeAcpdService _service; 

        public MyofficeAcpdController(IMyofficeAcpdService service)
        {
            _service = service;
        }

        // GET: api/myofficeacpd
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyOfficeAcpd>>> GetMyOfficeAcpds()
        {
            var myOfficeAcpds = await _service.GetAllMyOfficeAcpdsAsync();
            if (myOfficeAcpds == null || !myOfficeAcpds.Any())
            {
                return NotFound(); 
            }
            return Ok(myOfficeAcpds);
        }

        // GET: api/myofficeacpd/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MyOfficeAcpd>> GetMyOfficeAcpd(string id)
        {
            var myOfficeAcpd = await _service.GetMyOfficeAcpdByIdAsync(id);

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
            var createdAcpd = await _service.CreateMyOfficeAcpdAsync(myOfficeAcpd);

            return CreatedAtAction(nameof(GetMyOfficeAcpd), new { id = createdAcpd.AcpdSid }, createdAcpd);
        }

        // PUT: api/myofficeacpd/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMyOfficeAcpd(string id, MyOfficeAcpd myOfficeAcpd)
        {
            var updated = await _service.UpdateMyOfficeAcpdAsync(id, myOfficeAcpd);

            if (!updated)
            {
                if (!await _service.MyOfficeAcpdExistsAsync(id)) 
                {
                    return NotFound();
                }
                return BadRequest();
            }

            return NoContent(); 
        }

        // DELETE: api/myofficeacpd/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyOfficeAcpd(string id)
        {
            var deleted = await _service.DeleteMyOfficeAcpdAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
