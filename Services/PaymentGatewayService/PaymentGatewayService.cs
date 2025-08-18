using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PaymentApi.Models;
using PaymentApi.Services.PaymentGatewayService;

namespace PaymentApi.Services.PaymentGatewayService
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        private readonly HttpClient _httpClient;

        public PaymentGatewayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ConnectToPayment(MidtransCredential midtransCredential, object payload, string endPointUrl)
        {
            var jsonPayload = JsonSerializer.Serialize(payload);

            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{midtransCredential.ServerKey}:"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endPointUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Midtrans error: {responseContent}",
                    null,
                    response.StatusCode
                );
            }

            return responseContent;
        }
    }
}
