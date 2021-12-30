using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server.Behaviors
{
    internal class ManagerBehavior : WebSocketBehavior
    {
        User UserData;
        
        protected override void OnOpen()
        {
            Worker.Logger.LogInformation("Manager connected");
        }

        protected override void OnMessage(MessageEventArgs e)
        {

        }

        protected override void OnClose(CloseEventArgs e)
        {
            Worker.Logger.LogInformation("Manager disconnected");
        }

        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {

        }
    }
}
