using PaymentApi.Models;

namespace PaymentApi.Repositories.MidtransCredentialRepository
{
    public interface IMidtransCredentialRepository
    {
        public Task<MidtransCredential?> GetById(Guid guid);
    }
}
