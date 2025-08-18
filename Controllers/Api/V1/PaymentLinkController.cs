using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.DTOs.PaymentLink;
using PaymentApi.Helpers;
using PaymentApi.Repositories.MidtransCredentialRepository;
using PaymentApi.Services.PaymentLinkService;

namespace PaymentApi.Controllers.Api.V1
{
    [ApiController]
    [Route("api/v1/payment-link")]
    public class PaymentLinkController(IPaymentLinkService paymentLinkService, IMidtransCredentialRepository midtransCredentialRepository) : ApiResponser
    {
        private readonly IPaymentLinkService _paymentLinkService = paymentLinkService;
        private readonly IMidtransCredentialRepository _midtransCredentialRepository = midtransCredentialRepository;

        [HttpPost]
        public async Task<IActionResult> Store([FromBody, Required] PaymentLinkCreateDTO paymentLinkCreateDTO)
        {
            var credential = await _midtransCredentialRepository.GetById(paymentLinkCreateDTO.CredentialId);
            if (credential == null)
                return ErrorResponse("credential not found", 404, 40400);

            try
            {
                var paymentLink = await _paymentLinkService.CreatePaymentLink(credential, paymentLinkCreateDTO);
                var json = JsonSerializer.Deserialize<object>(paymentLink);
                return ShowOne(json!);
            }
            catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
            {
                var (message, statusCode, metaCode) = MidtransErrorHandler.Handle(ex);
                return ErrorResponse(message, statusCode, metaCode);
            }
        }

    }
}
