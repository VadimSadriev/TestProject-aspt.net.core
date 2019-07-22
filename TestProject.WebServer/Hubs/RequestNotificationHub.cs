using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TestProject.WebServer
{
    [Authorize(Roles = "Admin")]
    public class RequestNotificationHub : Hub
    {
        public async Task NotifyAdmins(string message)
        {
            await this.Clients.All.SendAsync("NotifyAdmins", message);
        }
    }
}
