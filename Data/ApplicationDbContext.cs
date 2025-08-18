using Microsoft.EntityFrameworkCore;
using PaymentApi.Models;

namespace PaymentApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MidtransCredential> MidtransCredentials { get; set; }
        public DbSet<PaymentLinkCharge> PaymentLinkCharges { get; set; }
        public DbSet<MidtransRequestLog> MidtransRequestLogs { get; set; }
        public DbSet<MidtransResponseLog> MidtransResponseLogs { get; set; }

    }
}
