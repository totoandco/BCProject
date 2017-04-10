
using System.Net.Sockets;
using BCProject.Resources;

namespace BCProject.Domain
{
    /// <summary>
    /// Class used to include different parameters in the IAsyncResult
    /// </summary>
    public class ClientObject 
    {
        
        /// <summary>
        /// The client socket
        /// </summary>
        public Socket CSocket { get; set; } = null;
        /// <summary>
        ///Flag for the handshake (completed or not)
        /// </summary>
        public bool IsHandshakeCompleted { get; set; } = false;
        /// <summary>
        /// Flag if the user has requested to terminate its session
        /// </summary>
        public bool IsSessionTerminated { get; set; } = false;
        /// <summary>
        /// The  buffer used for receiving data
        /// </summary>
        public byte[] RBuffer { get; set; } = new byte[Globals.BufferSize];
        /// <summary>
        /// The  buffer for the data to be sent
        /// </summary>
        public byte[] SBuffer { get; set; }

        public ClientObject getNewInstance()
        {
            return new ClientObject();
        }
    }
}
