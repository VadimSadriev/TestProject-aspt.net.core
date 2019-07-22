using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core
{
    public class EmailTemplateSender : IEmailTemplateSender
    {
        public async Task<SendEmailResponse> SendEmailVerificationAsync(SendEmailDetails details, string title, string content1, string content2, string buttonText, string buttonUrl)
        {
            var templateText = default(string);

            using (var reader = new 
                StreamReader(Path.Combine(DI.Environment.WebRootPath, "EmailTemplates/VefifyEmailTemplate.html"),
                Encoding.UTF8))
            {
                templateText = await reader.ReadToEndAsync();
            }

            templateText = templateText
                .Replace("--Title--", title)
                .Replace("--Content1--", content1)
                .Replace("--Content2--", content2)
                .Replace("--ButtonText--", buttonText)
                .Replace("--ButtonUrl--", buttonUrl);

            details.Content = templateText;

            var emailSender = DI.EmailSender;

            return await DI.EmailSender.SendEmailAsync(details);
        }
    }
}
