using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LoanProcessingApp.Dtos;
using LoanProcessingApp.Services;

namespace LoanProcessingApp.Controllers
{
    [ApiController]
    [Route("api/borrowers")]
    public class BorrowersController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;

        public BorrowersController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        /// <summary>
        /// Registers a new borrower.
        /// </summary>
        [HttpPost]
        public ActionResult<BorrowerResponse> Register([FromBody] BorrowerRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var response = _borrowerService.Register(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Retrieves a registered borrower by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public ActionResult<BorrowerResponse> GetById(Guid id)
        {
            var borrower = _borrowerService.GetById(id);
            return borrower == null ? NotFound() : Ok(borrower);
        }

        /// <summary>
        /// Retrieves all registered borrowers.
        /// </summary>
        [HttpGet]
        public ActionResult<IReadOnlyList<BorrowerResponse>> GetAll()
        {
            return Ok(_borrowerService.GetAll());
        }
    }
}
