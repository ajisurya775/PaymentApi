using Microsoft.EntityFrameworkCore;
using PaymentApi.Data;
using PaymentApi.Models;

namespace PaymentApi.Repositories.PaymentLinkChargeRepository
{
    public class PaymentLinkChargeRepository : IPaymentLinkChargeRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentLinkChargeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentLinkCharge> AddAsync(PaymentLinkCharge entity)
        {
            _context.Set<PaymentLinkCharge>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<PaymentLinkCharge>> GetAllAsync()
        {
            return await _context.Set<PaymentLinkCharge>().ToListAsync();
        }

        public async Task<PaymentLinkCharge?> GetByIdAsync(long id)
        {
            return await _context.Set<PaymentLinkCharge>().FindAsync(id);
        }
    }
}
