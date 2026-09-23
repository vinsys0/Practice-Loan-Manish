using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LoanProcessingApp.Dtos;
using LoanProcessingApp.Services;

namespace LoanProcessingApp.Controllers
{
    [ApiController]
    [Route("api/loan-bookings")]
    public class LoanBookingsController : ControllerBase
    {
        private readonly ILoanBookingService _loanBookingService;

        public LoanBookingsController(ILoanBookingService loanBookingService)
        {
            _loanBookingService = loanBookingService;
        }

        /// <summary>
        /// Books a loan for a registered borrower, allocating a lender using the
        /// First-Out or Last-Out strategy and applying that lender's interest rate.
        /// </summary>
        [HttpPost]
        public ActionResult<LoanBookingResponse> Book([FromBody] LoanBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var response = _loanBookingService.Book(request);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a loan booking by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public ActionResult<LoanBookingResponse> GetById(Guid id)
        {
            var booking = _loanBookingService.GetById(id);
            return booking == null ? NotFound() : Ok(booking);
        }

        /// <summary>
        /// Retrieves all loan bookings.
        /// </summary>
        [HttpGet]
        public ActionResult<IReadOnlyList<LoanBookingResponse>> GetAll()
        {
            return Ok(_loanBookingService.GetAll());
        }
    }
}
