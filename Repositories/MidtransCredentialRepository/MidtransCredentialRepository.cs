using Microsoft.EntityFrameworkCore;
using PaymentApi.Data;
using PaymentApi.Models;

namespace PaymentApi.Repositories.MidtransCredentialRepository
{
    public class MidtransCredentialRepository : IMidtransCredentialRepository
    {
        private readonly ApplicationDbContext _context;

        public MidtransCredentialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MidtransCredential?> GetById(Guid guid)
        {
            return await _context.MidtransCredentials.FirstOrDefaultAsync(m => m.Id == guid);
        }
    }
}
