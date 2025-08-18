using PaymentApi.DTOs.PaymentLink;
using PaymentApi.Models;

namespace PaymentApi.Services.PaymentLinkService
{
    public interface IPaymentLinkService
    {
        public Task<string> CreatePaymentLink(MidtransCredential midtransCredential, PaymentLinkCreateDTO paymentLinkCreateDto);
    }
}
