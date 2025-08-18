using PaymentApi.Models;

namespace PaymentApi.Services.PaymentGatewayService
{
    public interface IPaymentGatewayService
    {
        public Task<string> ConnectToPayment(MidtransCredential midtransCredential, object Payload, string EndPointUrl);
    }
}
