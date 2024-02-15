using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VenueFinder.Application.Interfaces;
using VenueFinder.Domain.Entities;

namespace VenueFinder.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VenueCategoriesController : ControllerBase
    {
        private readonly IVenueCategoryService _venueCategoryService;

        public VenueCategoriesController(IVenueCategoryService venueCategoryService)
        {
            _venueCategoryService = venueCategoryService;
        }

        // GET: api/VenueCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VenueCategory>>> GetAllAsync()
        {
            var venues = await _venueCategoryService.GetAllAsync();
            return Ok(venues);
        }

        // GET: api/VenueCategories/food
        [HttpGet("{name}")]
        public async Task<ActionResult<Venue>> GetVenue(string name)
        {
            var venueCategory = await _venueCategoryService.GetByNameAsync(name);

            if (venueCategory == null)
            {
                return NotFound();
            }

            return Ok(venueCategory);
        }

        // POST: api/VenueCategories
        [HttpPost]
        public async Task<ActionResult<Venue>> CreateVenue(VenueCategory venueCategory)
        {
            var createdVenueCategory = await _venueCategoryService.CreateAsync(venueCategory);
            return CreatedAtAction(nameof(GetVenue),
                new { id = createdVenueCategory.Id }, createdVenueCategory);
        }

        // PUT: api/VenueCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenue(string id, VenueCategory venueCategory)
        {
            if (!id.Equals(venueCategory.Id))
            {
                return BadRequest();
            }

            await _venueCategoryService.UpdateAsync(venueCategory);
            return NoContent();
        }

        // DELETE: api/VenueCategories/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteVenue(string name)
        {
            var venueCategory = await _venueCategoryService.GetByNameAsync(name);
            if (venueCategory == null)
            {
                return NotFound();
            }

            await _venueCategoryService.DeleteAsync(name);
            return NoContent();
        }
    }
}
