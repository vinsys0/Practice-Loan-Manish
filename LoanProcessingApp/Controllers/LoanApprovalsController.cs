using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LoanProcessingApp.Dtos;
using LoanProcessingApp.Services;

namespace LoanProcessingApp.Controllers
{
    /// <summary>
    /// Module for loan approvers to review and approve/reject pending loan bookings.
    /// </summary>
    [ApiController]
    [Route("api/loan-approvals")]
    public class LoanApprovalsController : ControllerBase
    {
        private readonly ILoanApprovalService _loanApprovalService;

        public LoanApprovalsController(ILoanApprovalService loanApprovalService)
        {
            _loanApprovalService = loanApprovalService;
        }

        /// <summary>
        /// Retrieves all loan bookings pending approval.
        /// </summary>
        [HttpGet("pending")]
        public ActionResult<IReadOnlyList<LoanBookingResponse>> GetPendingApprovals()
        {
            return Ok(_loanApprovalService.GetPendingApprovals());
        }

        /// <summary>
        /// Approves or rejects a pending loan booking.
        /// </summary>
        [HttpPost("decide")]
        public ActionResult<LoanBookingResponse> Decide([FromBody] LoanApprovalRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var response = _loanApprovalService.Decide(request);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
