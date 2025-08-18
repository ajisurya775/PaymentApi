using Microsoft.EntityFrameworkCore;
using PaymentApi.Data;
using PaymentApi.Models;

namespace PaymentApi.Repositories.MidtransResponseLogRepository
{
    public class MidtransResponseLogRepository : IMidtransResponseLogRepository
    {
        private readonly ApplicationDbContext _context;

        public MidtransResponseLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MidtransResponseLog> AddAsync(MidtransResponseLog entity)
        {
            _context.Set<MidtransResponseLog>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<MidtransResponseLog>> GetAllAsync()
        {
            return await _context.Set<MidtransResponseLog>().ToListAsync();
        }

        public async Task<MidtransResponseLog?> GetByIdAsync(long id)
        {
            return await _context.Set<MidtransResponseLog>().FindAsync(id);
        }
    }
}
