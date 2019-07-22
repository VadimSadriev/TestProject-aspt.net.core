using TestProject.Core;

namespace TestProject.WebServer
{
    public static class TestProjectEmailSender
    {
        public static async void SendAccountPasswordEmailAsync(string email, string password)
        {
            await DI.EmailSender.SendEmailAsync(new SendEmailDetails
            {
                FromEmail = DI.Configuration["EmailSettings:FromEmail"],
                FromName = DI.Configuration["EmailSettings:FromName"],
                IsHtml = true,
                Subject = "Account password",
                ToEmail = email,
                Content = $"Пароль от вашей учётной записи - {password}"
            });
        }
    }
}
