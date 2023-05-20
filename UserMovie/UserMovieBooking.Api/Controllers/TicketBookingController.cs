using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserMovieBooking.Domain.Models;
using UserMovieBooking.Domain.Services.Interfaces;

namespace UserMovieBooking.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/movie")]
    [ApiController]
    public class TicketBookingController : ControllerBase
    {
        private readonly ITicketBookingService _ticketBookingService;

        public TicketBookingController(ITicketBookingService ticketBookingService)
        {
            _ticketBookingService = ticketBookingService;
        }

        [Route("search/{movieName}")]
        [HttpGet]
        public async Task<ActionResult<IList<MovieModel>>> GetMovies(string movieName)
        {
            if (string.IsNullOrWhiteSpace(movieName))
            {
                return BadRequest();
            }

            return Ok(await _ticketBookingService.GetMoviesAsync(movieName));
        }

        [Route("booking/{movieId}")]
        [HttpPost]
        public async Task<ActionResult<string>> AddMovieTicket(TicketBookingModel ticketBookingModel)
        {
            if(ticketBookingModel == null)
            {
                return BadRequest();
            }

            return Ok(await _ticketBookingService.AddMovieTicketAsync(ticketBookingModel));
        }

        [Route("ticket/{ticketNumber}")]
        [HttpGet]
        public async Task<ActionResult<TicketBookingModel>> GetBookedTicketDetails(string ticketNumber)
        {
            if (string.IsNullOrWhiteSpace(ticketNumber))
            {
                return BadRequest();
            }

            return Ok(await _ticketBookingService.GetBookedTicketDetailsAsync(ticketNumber));
        }

        [Route("booking/history/{email}")]
        [HttpGet]
        public async Task<ActionResult<IList<TicketBookingModel>>> GetBookedTickedHistory(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }

            return Ok(await _ticketBookingService.GetBookedTickedHistoryAsync(email));
        }

        [Route("booking/cancel/{ticketNumber}")]
        [HttpGet]
        public async Task<ActionResult<IList<MovieModel>>> CancelBookedTicket(string ticketNumber)
        {
            if (string.IsNullOrWhiteSpace(ticketNumber))
            {
                return BadRequest();
            }

            return Ok(await _ticketBookingService.CancelBookedTicketAsync(ticketNumber));
        }

        [Authorize]
        [Route("booking/download/{ticketNumber}")]
        [HttpPost]
        public ActionResult<string> DownloadBookedTicket(string ticketNumber)
        {
            if (string.IsNullOrWhiteSpace(ticketNumber))
            {
                return BadRequest();
            }

            return Ok("Download Successful!");
        }
    }
}
