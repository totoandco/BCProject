using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using BCProject.Answers;
using BCProject.Resources;

namespace BCProject.Domain
{
    /// <summary>
    /// AsyncServer class is an implementation of an Asynchronous TCP server listening on the Port 55555
    /// it's designed to send an answer if the client sends one of the following commands:
    /// HELO -> complete the handshake and make available the other commands (wait 5s before the answer)
    /// COUNT -> request the number of successful handshakes with this server
    /// CONNECTIONS -> request the number of current connections on the server
    /// PRIME -> request a randomly generated prime number
    /// TERMINATE -> terminates the connection with the server
    /// It implements IDisposable as it's using the unmanaged class socket.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class AsyncServer : IDisposable
    {

        /// <summary>
        /// Gets or sets the set of answers.
        /// This Interface represents the set of answers we have for the commands specified in the specifications
        /// </summary>
        /// <value>
        /// The set of answers.
        /// </value>
        public ISetOfAnswers SetOfAnswers { get; set; }
        /// <summary>
        /// Gets or sets the handshakes counter.
        /// </summary>
        /// <value>
        /// The handshakes counter.
        /// </value>
        public static int HandshakesCounter { get; set; }
        /// <summary>
        /// The socket server
        /// </summary>
        private Socket SocketServer { get; set; }
        /// <summary>
        /// The list socket containing all the connected clients sockets.
        /// </summary>
        public List<Socket> ListSocket { get; set; }

  

        /// <summary>
        /// Callback of the async connection function 
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        private void ConnexionAcceptCallback(IAsyncResult asyncResult)
        {
            try
            {
                ClientObject cObject = new ClientObject();
                cObject.CSocket = ((Socket)asyncResult.AsyncState).EndAccept(asyncResult);
                ListSocket = CleanSocketList(ListSocket);
                ListSocket.Add(cObject.CSocket);
                cObject.CSocket.BeginReceive(cObject.RBuffer, 0, cObject.RBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), cObject);
                SocketServer.BeginAccept(new AsyncCallback(ConnexionAcceptCallback), SocketServer); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// <summary>
        /// Callback of the Async Receive function.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                ClientObject cObject = (ClientObject)asyncResult.AsyncState;
                int read = cObject.CSocket.EndReceive(asyncResult);
                string commandRead;
                if (read != 0)
                {
                    commandRead = Encoding.ASCII.GetString(cObject.RBuffer, 0, read);
                    //Perform actions if the command requires it
                    if (commandRead.Equals(AvailableCommands.HELO) && !cObject.IsHandshakeCompleted)
                    {
                        //flag that the client has completed the Handshake
                        cObject.IsHandshakeCompleted = true;
                        HandshakesCounter++;
                        Wait(Globals.TimeOutHeloCommand);
                    }
                    else if (commandRead.Equals(AvailableCommands.TERMINATE) && cObject.IsHandshakeCompleted)
                    {
                        //flag the client in order to proceed to its disconnection
                        cObject.IsSessionTerminated = true;
                    }
                    else if (commandRead.Equals(AvailableCommands.CONNECTIONS) && cObject.IsHandshakeCompleted)
                    {
                        ListSocket = CleanSocketList(ListSocket);
                    }
                    //get the command's answer  in the buffer
                    cObject.SBuffer = GetAnswer(commandRead, cObject.IsHandshakeCompleted);
                }
                //if there is something to send , send it and clear the buffer after
                if (cObject.SBuffer.Length != 0)
                {
                    cObject.CSocket.BeginSend(cObject.SBuffer, 0, cObject.SBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), cObject);
                    Array.Clear(cObject.SBuffer, 0, cObject.SBuffer.Length);
                }
                //Re-arm the async operation Receive
                cObject.CSocket.BeginReceive(cObject.RBuffer, 0, cObject.RBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), cObject);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
          
        }
        /// <summary>
        /// Callback  used for the Asyn  Send function .
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        private void SendCallback(IAsyncResult asyncResult)
        {
            try
            {
                ClientObject cObject = (ClientObject)asyncResult.AsyncState;
                cObject.CSocket.EndSend(asyncResult);

                //if Session is terminated then close the Socket of the client 
                if (cObject.IsSessionTerminated)
                {
                    CloseClientSocket(cObject);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncServer" /> class.
        /// Set the only setOfAnswer available
        /// </summary>
        public AsyncServer()
        {
            SetOfAnswers = new SetOfAnswers1();
            ListSocket = new List<Socket>();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            try
            {
                SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketServer.Bind(new IPEndPoint(Globals.IPAdressServer, Globals.PortNumber));
                SocketServer.Listen(Globals.ListenBackLog);
                //arm the asyn function to be ready for the first connection
                SocketServer.BeginAccept(new AsyncCallback(ConnexionAcceptCallback), SocketServer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Closes the  client socket.
        /// </summary>
        /// <param name="c_object">The c object.</param>
        public void CloseClientSocket(ClientObject cObject)
        {
            //wait in order to let the time for the last message to be displayed on the client side
            Wait(Globals.TimeOutBeforeDisconnection);
            ListSocket.Remove(cObject.CSocket);
            cObject.CSocket.Shutdown(SocketShutdown.Send);
            cObject.CSocket.Dispose();
          

        }
        /// <summary>
        /// Waits the specified number of millisecond.
        /// </summary>
        /// <param name="millisecond">The millisecond.</param>
        public void Wait(int millisecond)
        {
            Thread.Sleep(millisecond);
        }

        /// <summary>
        /// Get the answer.
        /// </summary>
        /// <param name="commandRead">The command read.</param>
        /// <param name="IsHandshakeCompleted">if set to <c>true</c> [is handshake completed].</param>
        /// <returns></returns>
        public byte[] GetAnswer(string commandRead, bool 
            
            IsHandshakeCompleted)
        {
            byte[] bufferResult= new byte[Globals.BufferSize];
            //depending of the command and isHandshakeCompleted flag chose the answer to put in the buffer to be sent
            if (IsHandshakeCompleted)
            { 

                if (commandRead.Equals(AvailableCommands.HELO) )
                {
                bufferResult = SetOfAnswers.HeloCommandAnswer();
                }
                else if (commandRead.Equals(AvailableCommands.COUNT))
                {
                    bufferResult = SetOfAnswers.CountCommandAnswer(HandshakesCounter);
                }
                else if (commandRead.Equals(AvailableCommands.CONNECTIONS))
                {
                   //get the number of connections directly with the list
                    bufferResult = SetOfAnswers.ConnectionsCommandAnswer(ListSocket.Count);
                }
                else if (commandRead.Equals(AvailableCommands.PRIME))
                {
                    bufferResult = SetOfAnswers.PrimeCommandAnswer();
                }
                else if (commandRead.Equals(AvailableCommands.TERMINATE))
                {
                    bufferResult = SetOfAnswers.TerminateCommandAnswer();

                }
            }
            return bufferResult;
        }
        /// <summary>
        /// Cleans the socket list if needed.
        /// This method prevent to keep Socket not connected anymore (if the client has closed the connection without the TERMINATE command)
        /// </summary>
        /// <param name="listSocket">The list socket.</param>
        /// <returns></returns>
        public List<Socket> CleanSocketList(List<Socket> listSocket)
         {
            int index = 0;
            int listLength = listSocket.Count;
           
            while (index < listLength)
            {
                Socket s = listSocket[index];
                if (!s.Connected)
                {
                    listSocket.RemoveAt(index);
                    s.Dispose();
                    listLength--;
                }
                index++;
            }
            return listSocket;
        }

        /// <summary>
        /// Implementation of the dispose method of the IDisposable interface
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);   
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="b"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool b)
        {
            if (b)
                SocketServer.Dispose();
        }
    }
}
