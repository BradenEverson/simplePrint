using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simplePrint
{
    public class ImageHub : Hub
    {
        public async Task sendImage(string user, string imageUrl)
        {
            await this.Clients.Caller.SendAsync("receiveImage",user,imageUrl);
        }
    }
}
