using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Core
{
    public class SendGridEmailSender : IEmailSender
    {
        public async Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details)
        {
            var apiKey = DI.Configuration["SendGrid:Key"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(details.FromEmail, details.FromName);
            var to = new EmailAddress(details.ToEmail, details.ToName);
            var subject = details.Subject;

            var content = details.Content;

            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                details.IsHtml ? null : details.Content,
                details.IsHtml ? details.Content : null);
            
            var response = await client.SendEmailAsync(msg);
          
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return new SendEmailResponse();
            }

            try
            {
                var bodyResult = await response.Body.ReadAsStringAsync();
                var sendGridResponse = JsonConvert.DeserializeObject<SendGridResponse>(bodyResult);

                var errorResponse = new SendEmailResponse
                {
                    Errors = sendGridResponse?.Errors.Select(f => f.Message).ToList()
                };

                if (errorResponse.Errors == null || errorResponse.Errors.Count() == 0)
                {
                    errorResponse.Errors = new List<string>() { "Неизвестная ошибка от службы отправки электронной почты. Пожалуйста, свяжитесь со службой поддержки" };
                }

                return errorResponse;

            }
            catch (Exception ex)
            {
                return new SendEmailResponse
                {
                    Errors = new List<string>() { "Произошла неизвестная ошибка" }
                };
            }


        }
    }
}
