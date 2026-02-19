using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Responses;
using DeepAzureServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeepAzureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbilityController : ControllerBase
    {
        private readonly IAbilityService _abilityService;

        public AbilityController(IAbilityService abilityService)
        {
            _abilityService = abilityService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<AbilityResponse>>> GetPagedAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize
        )
        {
            var result = await _abilityService.GetPagedAsync(pageNumber, pageSize);
            if (!result.Items.Any() || result.Items == null)
                return NoContent();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AbilityResponse>> GetByIdAsync(int id)
        {
            var result = await _abilityService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
