using Microsoft.AspNetCore.Mvc;
using LoanProcessingApp.Dtos;
using LoanProcessingApp.Services;
using System;
using System.Collections.Generic;

namespace LoanProcessingApp.Controllers
{
    [ApiController]
    [Route("api/lenders")]
    public class LendersController : ControllerBase
    {
        private readonly ILenderService _lenderService;

        public LendersController(ILenderService lenderService)
        {
            _lenderService = lenderService;
        }

        /// <summary>
        /// Registers a new lender.
        /// </summary>
        [HttpPost]
        public ActionResult<LenderResponse> Register([FromBody] LenderRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var response = _lenderService.Register(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Retrieves a registered lender by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public ActionResult<LenderResponse> GetById(Guid id)
        {
            var lender = _lenderService.GetById(id);
            return lender == null ? NotFound() : Ok(lender);
        }

        /// <summary>
        /// Retrieves all registered lenders.
        /// </summary>
        [HttpGet]
        public ActionResult<IReadOnlyList<LenderResponse>> GetAll()
        {
            return Ok(_lenderService.GetAll());
        }
    }
}
