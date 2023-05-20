using AdminMovieBooking.Domain.Models;
using AdminMovieBooking.Domain.Repository.Interfaces;
using AdminMovieBooking.Domain.Services.Interfaces;
using AdminMovieBooking.Infrastructure.Services;
using Moq;

namespace AdminMovieBooking.Tests
{
    public class InventoryServiceTests
    {
        private IInventoryService inventoryService;
        private Mock<IInventoryRepository> mockInventoryRepository;
        
        [SetUp]
        public void Setup()
        {
            mockInventoryRepository = new Mock<IInventoryRepository>();
            inventoryService = new InventoryService(mockInventoryRepository.Object);
        }

        [Test]
        public async Task GetMoviesAsync_Test()
        {
            //Arrange
            var response = "Movie Saved Successfully";
            InventoryInputParams inventoryInputParams = new InventoryInputParams 
            { 
                Name = "TestMovie", IsBlocked = false, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(3), TotalRows = 40
            };

            //mock
            mockInventoryRepository.Setup(x => x.AddMovieInventoryDetailsAsync(inventoryInputParams)).ReturnsAsync(response);

            //Act
            var result = await inventoryService.AddMovieInventoryDetailsAsync(inventoryInputParams);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.That(result, Is.EqualTo(response));
        }
    }
}