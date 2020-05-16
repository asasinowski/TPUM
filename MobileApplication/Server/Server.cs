using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            WebSocketServer webSocketServer = new WebSocketServer();
            webSocketServer.Start("http://localhost:80/test/");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
