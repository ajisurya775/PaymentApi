using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.DTOs.PaymentLink;

namespace PaymentApi.Controllers.Api.V1
{
    [ApiController]
    [Route("api/v1/payment-link")]
    public class PaymentLinkController : ControllerBase
    {
        [HttpPost]
        public IActionResult Store([FromBody, Required] PaymentLinkCreateDto paymentLinkCreateDto)
        {
            var data = new
            {
                Message = "Hello from PaymentLinkController"
            };
            return Ok(data);  // Pastikan "Ok" (huruf besar O)
        }
    }
}
