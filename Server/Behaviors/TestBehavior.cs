using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server.Behaviors
{
    internal class TestBehavior : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            Worker.Logger.LogInformation("Client connected");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Worker.Logger.LogInformation("Client sent message: " + e.Data);
            Send("Thank you!");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Worker.Logger.LogInformation("Client disconnected");
        }

        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            Worker.Logger.LogError("An error has occured. Message: " + e.Message);
        }
    }
}
