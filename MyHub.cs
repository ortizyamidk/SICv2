using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WPF_LoginForm
{
    public class MyHub : Hub
    {
        public void SendUpdate()
        {
            Clients.All.update();
        }
    }
}
