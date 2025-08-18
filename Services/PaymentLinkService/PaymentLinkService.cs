using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PaymentApi.Data;
using PaymentApi.DTOs.PaymentLink;
using PaymentApi.Models;
using PaymentApi.Repositories.MidtransRequestLogRepository;
using PaymentApi.Repositories.MidtransResponseLogRepository;
using PaymentApi.Repositories.PaymentLinkChargeRepository;
using PaymentApi.Services.PaymentGatewayService;

namespace PaymentApi.Services.PaymentLinkService
{
    public class PaymentLinkService : IPaymentLinkService
    {
        private readonly IPaymentGatewayService _paymentGatewayService;
        private readonly IMidtransRequestLogRepository _midtransRequestLogRepository;
        private readonly IMidtransResponseLogRepository _midtransResponseLogRepository;
        private readonly IPaymentLinkChargeRepository _paymentLinkChargeRepository;

        public PaymentLinkService(
            IPaymentGatewayService paymentGatewayService,
            IMidtransRequestLogRepository midtransRequestLogRepository,
            IMidtransResponseLogRepository midtransResponseLogRepository,
            IPaymentLinkChargeRepository paymentLinkChargeRepository
         )
        {
            _paymentGatewayService = paymentGatewayService;
            _midtransRequestLogRepository = midtransRequestLogRepository;
            _midtransResponseLogRepository = midtransResponseLogRepository;
            _paymentLinkChargeRepository = paymentLinkChargeRepository;
        }

        public async Task<string> CreatePaymentLink(MidtransCredential midtransCredential, PaymentLinkCreateDTO paymentLinkCreateDTO)
        {
            var payload = new
            {
                customer_details = new
                {
                    first_name = paymentLinkCreateDTO.Name,
                    email = paymentLinkCreateDTO.Email,
                    phone = paymentLinkCreateDTO.PhoneNumber
                },
                transaction_details = new
                {
                    order_id = paymentLinkCreateDTO.OrderId,
                    gross_amount = paymentLinkCreateDTO.ChargeAmount
                },
                credit_card = new
                {
                    secure = true
                }
            };

            var url = $"{midtransCredential.EndPointUrl}/snap/v1/transactions";

            // Log request ke database
            var midtransRequestLog = new MidtransRequestLog
            {
                Header = null,
                Body = JsonSerializer.Serialize(payload),
                Type = TypeEnum.CreateCharge
            };

            var request = await _midtransRequestLogRepository.AddAsync(midtransRequestLog);

            string paymentLink;
            try
            {
                paymentLink = await _paymentGatewayService.ConnectToPayment(midtransCredential, payload, url);

                var midtransResponseLog = new MidtransResponseLog
                {
                    Header = null,
                    Body = paymentLink,
                    MidtransRequestLogId = request.Id,
                    Type = TypeEnum.CreateCharge
                };

                await _midtransResponseLogRepository.AddAsync(midtransResponseLog);

                using var doc = JsonDocument.Parse(paymentLink);
                var paymentUrl = doc.RootElement.GetProperty("redirect_url").GetString();

                var paymentLinkCharge = new PaymentLinkCharge
                {
                    MidtransCredentialId = midtransCredential.Id,
                    OrderId = paymentLinkCreateDTO.OrderId,
                    ChargeAmount = paymentLinkCreateDTO.ChargeAmount,
                    Status = StatusEnumStrings.Pending,
                    CallBackUrl = paymentLinkCreateDTO.CallBackUrl,
                    PaymentUrl = paymentUrl!
                };

                await _paymentLinkChargeRepository.AddAsync(paymentLinkCharge);

                // Kembalikan response JSON ke controller
                return paymentLink;
            }
            catch (Exception ex)
            {
                // Jika error, tetap log response
                var midtransResponseLog = new MidtransResponseLog
                {
                    Header = null,
                    Body = ex.Message,
                    MidtransRequestLogId = request.Id,
                    Type = TypeEnum.CreateCharge
                };
                await _midtransResponseLogRepository.AddAsync(midtransResponseLog);

                // Lempar ulang exception agar bisa ditangani controller
                throw;
            }
        }
    }
}
