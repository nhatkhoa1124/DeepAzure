using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Interfaces;
using DeepAzureServer.Services.Implementations;
using FluentAssertions;
using Moq;

namespace DeepAzureServer.Tests.Services
{
    public class ElementServiceTests
    {
        private readonly Mock<IElementRepository> _elementRepoMock;
        private readonly ElementService _elementService;

        public ElementServiceTests()
        {
            _elementRepoMock = new Mock<IElementRepository>();
            _elementService = new ElementService(_elementRepoMock.Object);
        }

        // --- GET ALL ---
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllElements_WhenElementsExist()
        {
            var mockElements = new List<Element>
            {
                new Element{Id = 1, Name = "Fire"},
                new Element{Id = 2, Name = "Water"}
            };
            _elementRepoMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(mockElements);

            var result = await _elementService.GetAllAsync();

            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            _elementRepoMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoElementExists()
        {
            var mockElements = new List<Element>();

            _elementRepoMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(mockElements);

            var result = await _elementService.GetAllAsync();

            result.Should().BeNullOrEmpty();
            _elementRepoMock.Verify(x => x.GetAllAsync(), Times.Once);
        }


        // --- GET BY ID ---
        [Fact]
        public async Task GetByIdAsync_ShouldReturnValidObject_WhenElementExists()
        {
            int validId = 1;
            var mockElement = new Element
            {
                Id = 1,
                Name = "Fire"
            };
            _elementRepoMock.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(mockElement);

            var result = await _elementService.GetByIdAsync(validId);

            result.Should().NotBeNull();
            result.Id.Should().Be(validId);
            result.Name.Should().Be("Fire");
            _elementRepoMock.Verify(x => x.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenElementIdDoesNotExist()
        {
            int invalidId = -1;

            _elementRepoMock.Setup(repo => repo.GetByIdAsync(invalidId))
                .ReturnsAsync((Element) null);

            var result = await _elementService.GetByIdAsync(invalidId);

            result.Should().BeNull();
            _elementRepoMock.Verify(x => x.GetByIdAsync(invalidId), Times.Once);
        }
    }
}
