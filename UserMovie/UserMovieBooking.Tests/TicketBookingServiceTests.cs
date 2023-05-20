using Azure;
using Moq;
using UserMovieBooking.Domain.Models;
using UserMovieBooking.Domain.Repository.Interfaces;
using UserMovieBooking.Domain.Services.Interfaces;
using UserMovieBooking.Infrastructure.Repositories;
using UserMovieBooking.Infrastructure.Services;

namespace UserMovieBooking.Tests
{
    public class TicketBookingServiceTests
    {
        private ITicketBookingService ticketBookingService;
        private Mock<ITicketBookingRespository> mockTicketBookingRespository;

        [SetUp]
        public void Setup()
        {
            mockTicketBookingRespository = new Mock<ITicketBookingRespository>();
            ticketBookingService = new TicketBookingService(mockTicketBookingRespository.Object);
        }

        [Test]
        public async Task AddMovieTicketAsync_Test()
        {
            //Arrange
            TicketBookingModel ticketBookingModel = new TicketBookingModel() 
            {
                MovieDetails = new MovieDetails() { Name = "TesTMovie", Place = "BHO", StartTime =  DateTime.Now.AddHours(1), EndTime = DateTime.Now.AddHours(3) },
                UserDetails = new UserDetails() { UserName = "TestUser", Age = 20, NumberOfSeats = 1, UserEmail = "testuser@abc.com"},
                TicketDetails = new TicketDetails() { IsCancelled = false, TicketNumber = "NA", TotalPrice = 250 }
            };
            var response = "Ticket details Saved Successfully!";

            //mock
            mockTicketBookingRespository.Setup(x => x.AddMovieTicketAsync(ticketBookingModel)).ReturnsAsync(response);

            //Act
            var result = await ticketBookingService.AddMovieTicketAsync(ticketBookingModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async Task CancelBookedTicketAsync_Test()
        {
            //Arrange
            string ticketNumber = "TIC101";
            var response = "Ticket Cancelled Successfully!";

            //mock
            mockTicketBookingRespository.Setup(x => x.CancelBookedTicketAsync(ticketNumber)).ReturnsAsync(response);

            //Act
            var result = await ticketBookingService.CancelBookedTicketAsync(ticketNumber);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async Task GetBookedTickedHistoryAsync_Test()
        {
            //Arrange
            string ticketNumber = "TIC101";
            TicketBookingModel ticketBookingModel = new TicketBookingModel()
            {
                MovieDetails = new MovieDetails() { Name = "TesTMovie", Place = "BHO", StartTime = DateTime.Now.AddHours(1), EndTime = DateTime.Now.AddHours(3) },
                UserDetails = new UserDetails() { UserName = "TestUser", Age = 20, NumberOfSeats = 1, UserEmail = "testuser@abc.com" },
                TicketDetails = new TicketDetails() { IsCancelled = false, TicketNumber = "TIC101", TotalPrice = 250 }
            };
            var ticketBookingModelList = new List<TicketBookingModel>() { ticketBookingModel };            

            //mock
            mockTicketBookingRespository.Setup(x => x.GetBookedTickedHistoryAsync(ticketNumber)).ReturnsAsync(ticketBookingModelList);

            //Act
            var result = await ticketBookingService.GetBookedTickedHistoryAsync(ticketNumber);

            //Assert
            Assert.IsNotNull(result);            
            Assert.That(result, Is.EqualTo(ticketBookingModelList));
        }

        [Test]
        public async Task GetBookedTicketDetailsAsync_Test()
        {
            //Arrange
            string ticketNumber = "TIC101";
            TicketBookingModel ticketBookingModel = new TicketBookingModel()
            {
                MovieDetails = new MovieDetails() { Name = "TesTMovie", Place = "BHO", StartTime = DateTime.Now.AddHours(1), EndTime = DateTime.Now.AddHours(3) },
                UserDetails = new UserDetails() { UserName = "TestUser", Age = 20, NumberOfSeats = 1, UserEmail = "testuser@abc.com" },
                TicketDetails = new TicketDetails() { IsCancelled = false, TicketNumber = "TIC101", TotalPrice = 250 }
            };            

            //mock
            mockTicketBookingRespository.Setup(x => x.GetBookedTicketDetailsAsync(ticketNumber)).ReturnsAsync(ticketBookingModel);

            //Act
            var result = await ticketBookingService.GetBookedTicketDetailsAsync(ticketNumber);

            //Assert
            Assert.IsNotNull(result);            
            Assert.That(result, Is.EqualTo(ticketBookingModel));
        }

        [Test]
        public async Task GetMoviesAsync_Test()
        {
            //Arrange
            var movieName = "TestMovie";
            var movieList = new List<MovieModel>() { new MovieModel() { MovieName = movieName, MoviePlace = "BHO" } };

            //mock
            mockTicketBookingRespository.Setup(x => x.GetMoviesAsync(movieName)).ReturnsAsync(movieList);

            //Act
            var result = await ticketBookingService.GetMoviesAsync(movieName);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.That(result, Is.EqualTo(movieList));
        }
    }
}