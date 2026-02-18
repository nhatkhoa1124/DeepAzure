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
        public async Task<ActionResult<IEnumerable<MonsterResponse>>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20
        )
        {
            var pagedMonsters = await _monsterService.GetAllAsync(pageNumber, pageSize);
            var monsters = pagedMonsters.Items;
            if (!monsters.Any())
            {
                return NoContent();
            }
            var response = monsters.Select(m => m.ToResponseDto()).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MonsterResponse>> GetByIdAsync(int id)
        {
            var monster = await _monsterService.GetByIdAsync(id);
            if (monster == null)
                return NotFound();
            return Ok(monster.ToResponseDto());
        }
    }
}
