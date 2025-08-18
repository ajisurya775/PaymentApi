using PaymentApi.Models;

namespace PaymentApi.Repositories.PaymentLinkChargeRepository
{
    public interface IPaymentLinkChargeRepository
    {
        public Task<PaymentLinkCharge> AddAsync(PaymentLinkCharge entity);
        public Task<List<PaymentLinkCharge>> GetAllAsync();
        public Task<PaymentLinkCharge?> GetByIdAsync(long id);
    }
}
