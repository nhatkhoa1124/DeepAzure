using DeepAzureServer.Models.Responses;
using DeepAzureServer.Services.Implementations;
using DeepAzureServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeepAzureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementController : ControllerBase
    {
        private IElementService _elementService;

        public ElementController(IElementService elementService)
        {
            _elementService = elementService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElementResponse>>> GetAllAsync()
        {
            var result = await _elementService.GetAllAsync();
            if (!result.Any())
                return NoContent();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ElementResponse>> GetByIdAsync(int id)
        {
            var result = await _elementService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
