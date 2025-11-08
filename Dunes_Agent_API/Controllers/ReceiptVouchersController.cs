using Application.Dtos.ReceiptVoucher;
using Application.Services.ReceiptVoucher;
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
        public async Task<IActionResult> CreateReceiptVoucher([FromBody]AddNewReceiptVoucherRequest request)
        {
            if(!ModelState.IsValid) 
                return BadRequest("Invalid Request");

            var result = await receiptVoucherService.CreateReceiptVoucherAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value, request);

            if (!result)
                return StatusCode(500, "Service Error");

            return Ok("Receipt Voucher Created");
        }
    }
}
