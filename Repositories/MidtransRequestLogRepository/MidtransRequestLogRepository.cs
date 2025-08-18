using Microsoft.EntityFrameworkCore;
using PaymentApi.Data;
using PaymentApi.Models;

namespace PaymentApi.Repositories.MidtransRequestLogRepository
{
    public class MidtransRequestLogRepository : IMidtransRequestLogRepository
    {
        private readonly ApplicationDbContext _context;

        public MidtransRequestLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MidtransRequestLog> AddAsync(MidtransRequestLog entity)
        {
            _context.Set<MidtransRequestLog>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<MidtransRequestLog>> GetAllAsync()
        {
            return await _context.Set<MidtransRequestLog>().ToListAsync();
        }

        public async Task<MidtransRequestLog?> GetByIdAsync(long id)
        {
            return await _context.Set<MidtransRequestLog>().FindAsync(id);
        }
    }
}
