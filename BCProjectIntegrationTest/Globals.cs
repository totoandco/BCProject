using System.Net;

namespace BCProjectIntegrationTest
{
    /// <summary>
    /// Class used for the hardcoded values
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Gets or sets a waiting time.
        /// </summary>
        /// <value>
        /// The waiting time.
        /// </value>
        public static int WaitingTime { get; set; } = 1000;
        /// <summary>
        /// Gets or sets the waiting time after handshake.
        /// </summary>
        /// <value>
        /// The waiting time after handshake.
        /// </value>
        public static int WaitingTimeAfterHandshake { get; set; } = 6000;

        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        /// <value>
        /// The port number.
        /// </value>
        public static int PortNumber { get; set; } = 55555;
        /// <summary>
        /// Gets or sets the ip adress of the server.
        /// </summary>
        /// <value>
        /// The ip adress server.
        /// </value>
        public static IPAddress IPAdressServer{ get; set; } = IPAddress.Parse("127.0.0.1");

        /// <summary>
        /// Gets or sets the size of the buffer.
        /// </summary>
        /// <value>
        /// The size of the buffer.
        /// </value>
        public static int BufferSize { get; set; } = 1024;

        /// <summary>
        /// Gets or sets the helo command.
        /// </summary>
        /// <value>
        /// The helo command.
        /// </value>
        public static string  HeloCommand { get; set; }="HELO";
        /// <summary>
        /// Gets or sets the count command.
        /// </summary>
        /// <value>
        /// The count command.
        /// </value>
        public static string CountCommand { get; set; } = "COUNT";
        /// <summary>
        /// Gets or sets the connections command.
        /// </summary>
        /// <value>
        /// The connections command.
        /// </value>
        public static string ConnectionsCommand { get; set; } = "CONNECTIONS";
        /// <summary>
        /// Gets or sets the prime command.
        /// </summary>
        /// <value>
        /// The prime command.
        /// </value>
        public static string PrimeCommand { get; set; } = "PRIME";
        /// <summary>
        /// Gets or sets the terminate command.
        /// </summary>
        /// <value>
        /// The terminate command.
        /// </value>
        public static string TerminateCommand { get; set; } = "TERMINATE";
    }
}
