using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace App.Desafio.Blog.Crosscutting.Sockets
{
    public class BlogChannel : Hub
    {
        public async Task NotifyNewPost(string message)
        {
            Log.Information("Received Message", message);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
