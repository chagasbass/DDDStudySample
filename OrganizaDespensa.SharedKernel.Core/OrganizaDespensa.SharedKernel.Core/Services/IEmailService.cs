using System.Threading.Tasks;

namespace OrganizaDespensa.SharedKernel.Core.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string emailTo, string username, string emailText);
    }
}
