using PaymentApi.Models;

namespace PaymentApi.Repositories.MidtransResponseLogRepository
{
    public interface IMidtransResponseLogRepository
    {
        public Task<MidtransResponseLog> AddAsync(MidtransResponseLog entity);
        public Task<List<MidtransResponseLog>> GetAllAsync();
        public Task<MidtransResponseLog?> GetByIdAsync(long id);
    }
}
