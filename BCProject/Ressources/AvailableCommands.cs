

namespace BCProject.Resources
{
    /// <summary>
    /// Class having the different available commands
    /// </summary>
    public static class AvailableCommands
    {
        /// <summary>
        /// HELO -> complete the handshake and make available the other commands (wait 5s before the answer)
        /// </summary>
        public static string HELO { get; set; } = "HELO";
        
        /// <summary>
        ///  COUNT -> request the number of successful handshakes with this server
        /// </summary>
        public static string COUNT { get; set; } = "COUNT";
       
        /// <summary>
        /// CONNECTIONS -> request the number of current connections on the server
        /// </summary>
        public static string CONNECTIONS { get; set; } = "CONNECTIONS";
        
        /// <summary>
        /// PRIME -> request a randomly generated prime number
        /// </summary>
        public static string PRIME { get; set; } = "PRIME";
       
        /// <summary>
        /// TERMINATE -> terminates the connection with the server
        /// </summary>
        public static string TERMINATE { get; set; } = "TERMINATE";
    }
}
