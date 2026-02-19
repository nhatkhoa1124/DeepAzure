using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Responses;
using DeepAzureServer.Services.Interfaces;
using DeepAzureServer.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DeepAzureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonsterController : ControllerBase
    {
        private IMonsterService _monsterService;

        public MonsterController(IMonsterService monsterService)
        {
            _monsterService = monsterService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<MonsterResponse>>> GetPagedAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20
        )
        {
            var pagedMonsters = await _monsterService.GetPagedAsync(pageNumber, pageSize);
            if (!pagedMonsters.Items.Any() || pagedMonsters.Items == null)
                return NoContent();

            return Ok(pagedMonsters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MonsterResponse>> GetByIdAsync(int id)
        {
            var monster = await _monsterService.GetByIdAsync(id);
            if (monster == null)
                return NotFound();
            return Ok(monster);
        }
    }
}
