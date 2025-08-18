using PaymentApi.Models;

namespace PaymentApi.Repositories.MidtransRequestLogRepository
{
    public interface IMidtransRequestLogRepository
    {
        public Task<MidtransRequestLog> AddAsync(MidtransRequestLog entity);
        public Task<List<MidtransRequestLog>> GetAllAsync();
        public Task<MidtransRequestLog?> GetByIdAsync(long id);
    }
}
