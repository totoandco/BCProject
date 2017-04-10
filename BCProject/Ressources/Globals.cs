
using System.Linq;
using System.Net;

namespace BCProject.Resources
{
    /// <summary>
    /// Class used to regroup all the hard coded values.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// The buffer size
        /// </summary>
        public static int BufferSize {get;set;} = 1024 ;
        
        /// <summary>
        /// The ip adress of the server
        /// </summary>
        public static IPAddress IPAdressServer { get; set; } = IPAddress.Any;

        /// <summary>
        /// The port number used to listen (spec)
        /// </summary>
        public static int PortNumber { get; set; } = 55555;

        /// <summary>
        /// Gets or sets the listen backlog.
        /// </summary>
        /// <value>
        /// The listen back log.
        /// </value>
        public static int ListenBackLog { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the timeout helo command.
        /// </summary>
        /// <value>
        /// The time out helo command.
        /// </value>
        public static int TimeOutHeloCommand { get; set; } = 5000;

        /// <summary>
        /// Gets or sets the timeout before disconnection.
        /// </summary>
        /// <value>
        /// The time out before disconnection.
        /// </value>
        public static int TimeOutBeforeDisconnection { get; set; } = 1000;

    }
}
