using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace BCProjectIntegrationTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
      //Launch an Async Client in order to test the server
            AsynClient client = new AsynClient();
            Random r = new Random();
      // we name the client randomly
            client.Name = "client " + r.Next(0, Int32.MaxValue);
            client.Start();
      // wait to be sure that the connection is established
            Thread.Sleep(Globals.WaitingTime);

            client.SendHandshake();
      // wait the timeout of the handshake
            Thread.Sleep(Globals.WaitingTimeAfterHandshake);
      //send messages to the server
            for (int i = 0; i < 10000; i++)
            {
                client.SendMessages();
            }
            while (true) { }
        }
    }
}
