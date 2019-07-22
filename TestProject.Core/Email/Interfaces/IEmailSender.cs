using System.Threading.Tasks;

namespace TestProject.Core
{
    public interface IEmailSender
    {
        Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details);
    }
}
