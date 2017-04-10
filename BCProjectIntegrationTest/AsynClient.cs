using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace BCProjectIntegrationTest
{
    /// <summary>
    /// Async TCP Client to test an Async Server  
    /// </summary>
    class AsynClient
    {
        /// <summary>
        /// The  client Socket
        /// </summary>
        Socket sClient;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// The buffer
        /// </summary>
        public byte[] buffer = new byte[Globals.BufferSize];

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {

            try
            {
                sClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                sClient.BeginConnect(new IPEndPoint(Globals.IPAdressServer, Globals.PortNumber), new AsyncCallback(ConnectCallback), sClient);
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        ///callback for the Async Connect function  
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());
                Receive(client);
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(string message)
        {
            try
            {
               
                byte[] bufferToSend = Encoding.ASCII.GetBytes(message);
                Console.WriteLine("Client " + Name + " send : " + message);
                sClient.BeginSend(bufferToSend, 0, bufferToSend.Length, 0,new AsyncCallback(SendCallback), sClient);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// <summary>
        /// callback for the Async Send function  
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;        
                int bytesSent = client.EndSend(ar);
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// Receiving function 
        /// </summary>
        /// <param name="client">The client.</param>
        private void Receive(Socket client)
        {
            try
            {
                client.BeginReceive(buffer, 0, buffer.Length, 0,new AsyncCallback(ReceiveCallback), client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// callback used for the Receiving function 
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                   Console.WriteLine("Client "+Name+" received : "+Encoding.ASCII.GetString(buffer, 0, bytesRead));
                    Array.Clear(buffer,0, buffer.Length);
                    //re-arm the async function
                    client.BeginReceive(buffer, 0, Globals.BufferSize, 0,new AsyncCallback(ReceiveCallback), client);
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Sends the handshake.
        /// </summary>
        public void SendHandshake()
        {
            SendMessage(Globals.HeloCommand);

        }
        /// <summary>
        /// Sends the messages.
        /// </summary>
        public void SendMessages()
        {
            SendMessage(Globals.CountCommand);
            Thread.Sleep(Globals.WaitingTime);
            SendMessage(Globals.ConnectionsCommand);
            Thread.Sleep(Globals.WaitingTime);
            SendMessage(Globals.PrimeCommand);
            Thread.Sleep(Globals.WaitingTime);

        }
        /// <summary>
        /// Sends the terminate.
        /// </summary>
        public void SendTerminate()
        {
            SendMessage(Globals.TerminateCommand);

        }


    }
}

