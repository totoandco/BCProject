using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCProject.Answers
{
    /// <summary>
    /// This interface contains the answer to the different available commands 
    /// </summary>
    public interface ISetOfAnswers
    {
        /// <summary>
        /// Answer to  the command HELO.
        /// </summary>
        /// <returns></returns>
        byte[] HeloCommandAnswer();

        /// <summary>
        /// Answer to  the command COUNT.
        /// </summary>
        /// <param name="handshakesCounter">The handshakes counter.</param>
        /// <returns></returns>
        byte[] CountCommandAnswer(int handshakesCounter);

        /// <summary>
        /// Answer to  the command  Connections.
        /// </summary>
        /// <param name="connectionsCounter">The connections counter.</param>
        /// <returns></returns>
        byte[] ConnectionsCommandAnswer(int connectionsCounter);

        /// <summary>
        /// Answer to  the command  Prime
        /// </summary>
        /// <returns></returns>
        byte[] PrimeCommandAnswer();

        /// <summary>
        /// Answer to  the command  Terminate
        /// </summary>
        /// <returns></returns>
        byte[] TerminateCommandAnswer();
    }
}
