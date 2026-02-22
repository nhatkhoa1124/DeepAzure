using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Entities;
using DeepAzureServer.Models.Responses;
using DeepAzureServer.Repositories.Interfaces;
using DeepAzureServer.Services.Implementations;
using DeepAzureServer.Services.Interfaces;
using FluentAssertions;
using Moq;

namespace DeepAzureServer.Tests.Services
{
    public class MonsterServiceTests
    {
        private readonly Mock<IMonsterRepository> _monsterRepoMock;
        private readonly MonsterService _monsterService;

        public MonsterServiceTests()
        {
            _monsterRepoMock = new Mock<IMonsterRepository>();
            _monsterService = new MonsterService(_monsterRepoMock.Object);
        }

        // --- GET PAGED ---
        [Fact]
        public async Task GetPagedAsync_ShouldReturnPagedMonsters_WhenPageNumberAndPageSizeAreValid()
        {
            int validPageNumber = 1;
            int validPageSize = 10;
            int totalCount = 20;
            var mockMonsters = new PagedResult<Monster>
            {
                Items = new List<Monster>()
                {
                    new Monster
                    {
                        Id = 1,
                        Name = "Ignis",
                        PrimaryElement = new Element { Id = 1, Name = "Fire" },
                    },
                    new Monster
                    {
                        Id = 2,
                        Name = "Fishy",
                        PrimaryElement = new Element { Id = 2, Name = "Aqua" },
                    },
                },
                PageNumber = validPageNumber,
                PageSize = validPageSize,
                TotalCount = totalCount,
            };
            _monsterRepoMock
                .Setup(repo => repo.GetPagedAsync(validPageNumber, validPageSize))
                .ReturnsAsync(mockMonsters);

            var result = await _monsterService.GetPagedAsync(validPageNumber, validPageSize);

            result.Should().NotBeNull();
            result.Items.Should().NotBeNullOrEmpty();
            result.PageNumber.Should().Be(validPageNumber);
            result.PageSize.Should().Be(validPageSize);
            _monsterRepoMock.Verify(
                x => x.GetPagedAsync(validPageNumber, validPageSize),
                Times.Once
            );
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnEmptyItems_WhenNoMonstersExist()
        {
            int validPageNumber = 1;
            int validPageSize = 10;
            int totalCount = 20;
            var mockMonsters = new PagedResult<Monster>
            {
                Items = new List<Monster>() { },
                PageNumber = validPageNumber,
                PageSize = validPageSize,
                TotalCount = totalCount,
            };
            _monsterRepoMock
                .Setup(repo => repo.GetPagedAsync(validPageNumber, validPageSize))
                .ReturnsAsync(mockMonsters);

            var result = await _monsterService.GetPagedAsync(validPageNumber, validPageSize);

            result.Should().NotBeNull();
            result.Items.Should().BeNullOrEmpty();
            result.PageNumber.Should().Be(validPageNumber);
            result.PageSize.Should().Be(validPageSize);
            _monsterRepoMock.Verify(
                x => x.GetPagedAsync(validPageNumber, validPageSize),
                Times.Once
            );
        }

        // --- GET BY ID ---
        [Fact]
        public async Task GetByIdAsync_ShouldReturnValidObject_WhenElementExists()
        {
            int validId = 1;
            var mockElement = new Monster
            {
                Id = 1,
                Name = "Ignis",
                PrimaryElement = new Element { Id = 1, Name = "Fire" },
            };
            _monsterRepoMock.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(mockElement);

            var result = await _monsterService.GetByIdAsync(validId);

            result.Should().NotBeNull();
            result.Id.Should().Be(validId);
            result.Name.Should().Be("Ignis");
            _monsterRepoMock.Verify(x => x.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenElementIdDoesNotExist()
        {
            int invalidId = -1;

            _monsterRepoMock
                .Setup(repo => repo.GetByIdAsync(invalidId))
                .ReturnsAsync((Monster)null);

            var result = await _monsterService.GetByIdAsync(invalidId);

            result.Should().BeNull();
            _monsterRepoMock.Verify(x => x.GetByIdAsync(invalidId), Times.Once);
        }
    }
}
