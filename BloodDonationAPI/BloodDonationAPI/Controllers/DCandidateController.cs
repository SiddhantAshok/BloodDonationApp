using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BloodDonationAPI.Controllers
{
    [Route("api/DCandidate")]
    [ApiController]
    public class DCandidateController : ControllerBase
    {

        private readonly DonationDBContext _context;

        public DCandidateController(DonationDBContext context)
        {
            _context = context;
        }

        // GET: api/DCandidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DCandidate>>> GetDCandidates()
        {
            return await _context.DCandidates.ToListAsync();
        }

        // GET api/DCandidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DCandidate>> GetDCandidate(int id)
        {
            //return await _context.DCandidates.Where(c => c.id == id).FirstOrDefaultAsync();

            var dCandidate = await _context.DCandidates.FindAsync(id);

            if (dCandidate == null)
            {
                return NotFound();
            }

            return dCandidate;
        }

        // POST api/DCandidate
        [HttpPost]
        public async Task<ActionResult<DCandidate>> Post([FromBody] DCandidate dCandidate)
        {
            await _context.DCandidates.AddAsync(dCandidate);
            await _context.SaveChangesAsync();
            //awiat _context.SaveChangesAsync();

            return CreatedAtAction("GetDCandidate", new { id = dCandidate.id }, dCandidate);

        }

        // PUT api/DCandidate/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DCandidate dCandidate)
        {
            dCandidate.id = id;

            _context.Entry(dCandidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DCandidateExists(id))
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

        // DELETE api/DCandidate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DCandidate>> Delete(int id)
        {

            var dCandidate = await _context.DCandidates.FindAsync(id);
            if (dCandidate != null)
            {
                _context.DCandidates.Remove(dCandidate);
                await _context.SaveChangesAsync();
                return dCandidate;
            }
            else
            {
                return NotFound();
            }
        }

        private bool DCandidateExists(int id)
        {
            return _context.DCandidates.Any(c => c.id == id);
        }
    }
}
