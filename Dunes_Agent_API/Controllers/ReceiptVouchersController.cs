using Application.Dtos.ReceiptVoucher;
using Application.Services.ReceiptVoucher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptVouchersController(IReceiptVoucherService receiptVoucherService) : ControllerBase
    {
        [HttpPost]
        [Route("[Action]")]
        [Authorize]
        public async Task<IActionResult> CreateReceiptVoucher([FromBody]AddNewReceiptVoucherRequest request)
        {
            if(!ModelState.IsValid) 
                return BadRequest("Invalid Request");

            var result = await receiptVoucherService.CreateReceiptVoucherAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value, request);

            if (!result)
                return StatusCode(500, "Service Error");

            return Ok("Receipt Voucher Created");
        }

        [HttpPatch]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> UpdateReceiptVoucher(int id , UpdateReceiptVoucherRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Request");

            var result = await receiptVoucherService.PatchAsync(id,request);

            if (!result)
                return StatusCode(500, "Service error");

            return Ok("ReceiptVoucher Updated Succesfully .");
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> GetAllReceiptVoucher(string? guestName, string? agentName, string? LocationName, string? ServiceName,
            string sortColumn = "CreatedAt", bool isAscending = false, int page = 1, int pageSize = 6)
        {
            var Vouchers = await receiptVoucherService.GetAllPaginatedAsync(guestName, agentName, LocationName, ServiceName, sortColumn, isAscending, page);

            if (Vouchers.TotalCount < 1)
                return Ok("No Vouchers Exists");

            return Ok(Vouchers);
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> GetReceiptVoucherById(int id)
        {
            var voucher = await receiptVoucherService.GetByIdAsync(id);

            return Ok(voucher);
        }
    }
}
