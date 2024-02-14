using Microsoft.AspNetCore.Mvc;
using VenueFinder.Application.Interfaces;
using VenueFinder.Domain.Entities;

namespace VenueFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly IVenueService _venueService;

        public VenuesController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        // GET: api/Venues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenuesByCategory(string category)
        {
            var venues = await _venueService.GetVenuesByCategoryAsync(category);
            return Ok(venues);
        }

        // GET: api/Venues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenue(string id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            return Ok(venue);
        }

        // POST: api/Venues
        [HttpPost]
        public async Task<ActionResult<Venue>> CreateVenue(Venue venue)
        {
            var createdVenue = await _venueService.CreateVenueAsync(venue);
            return CreatedAtAction(nameof(GetVenue), new { id = createdVenue.Id }, createdVenue);
        }

        // PUT: api/Venues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenue(string id, Venue venue)
        {
            if (!id.Equals(venue.Id))
            {
                return BadRequest();
            }

            await _venueService.UpdateVenueAsync(venue);
            return NoContent();
        }

        // DELETE: api/Venues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(string id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);
            if (venue == null)
            {
                return NotFound();
            }

            await _venueService.DeleteVenueAsync(id);
            return NoContent();
        }
    }
}
