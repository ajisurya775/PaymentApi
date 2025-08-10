using PaymentApi.Models;
using PaymentApi.Data;
using System;
using System.Linq;

namespace PaymentApi.Data.Seeders
{
    public static class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.MidtransCredentials.Any())
            {
                context.MidtransCredentials.AddRange(
                    new MidtransCredential
                    {
                        Id = Guid.NewGuid(),
                        Name = "Midtrans Dev",
                        ClientKey = "SB-Mid-client-E-dt3aH5Q2h5uo7N",
                        ServerKey = "SB-Mid-server-rR_3WWxUlSs0OSyprfrWsWge",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new MidtransCredential
                    {
                        Id = Guid.NewGuid(),
                        Name = "Midtrans Prod",
                        ClientKey = "client-key-prod",
                        ServerKey = "server-key-prod",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
