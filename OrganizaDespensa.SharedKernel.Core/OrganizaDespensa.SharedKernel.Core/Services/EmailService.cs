using System.Threading.Tasks;

namespace OrganizaDespensa.SharedKernel.Core.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(string emailTo, string username, string emailText)
        {
            return true;
        }
    }
}
