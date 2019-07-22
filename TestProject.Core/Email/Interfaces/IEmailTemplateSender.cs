using System.Threading.Tasks;

namespace TestProject.Core
{
    public interface IEmailTemplateSender
    {
        Task<SendEmailResponse> SendEmailVerificationAsync(SendEmailDetails details, string title, string content1, string content2, string buttontText, string buttonUrl);
    }
}
