using System;
using System.Text;
using BCProject.Resources;

namespace BCProject.Answers
{
    /// <summary>
    /// One concret implementation of the ISetOfAnswers interface
    /// </summary>
    /// <seealso cref="BCProject.Commands.ISetOfAnswers" />
    public class SetOfAnswers1 : ISetOfAnswers
    {
        /// <summary>
        /// Answer to  the command  Connections.
        /// </summary>
        /// <param name="counter">The connections counter.</param>
        /// <returns></returns>
        public byte[] ConnectionsCommandAnswer(int counter)
        {
            return Encoding.ASCII.GetBytes(counter + Environment.NewLine);
        }

        /// <summary>
        /// Answer to  the command COUNT.
        /// </summary>
        /// <param name="counter">The handshakes counter.</param>
        /// <returns></returns>
        public byte[] CountCommandAnswer(int counter)
        {
            return Encoding.ASCII.GetBytes(counter + Environment.NewLine); 
        }

        /// <summary>
        /// Answer to  the command HELO.
        /// </summary>
        /// <returns></returns>
        public byte[] HeloCommandAnswer()
        {
            return Encoding.ASCII.GetBytes(AvailableAnswers.HeloAnswer + Environment.NewLine);
        }

        /// <summary>
        /// Answer to  the command  Prime
        /// </summary>
        /// <returns></returns>
        public byte[] PrimeCommandAnswer()
        {
            Random r = new Random();
            int n=0;
            while (!IsPrimeNumber(n))
            {
                n = r.Next(0, Int32.MaxValue);
            }
            return Encoding.ASCII.GetBytes(n + Environment.NewLine);

        }

        /// <summary>
        /// Answer to  the command  Terminate
        /// </summary>
        /// <returns></returns>
        public byte[] TerminateCommandAnswer()
        {
            return Encoding.ASCII.GetBytes(AvailableAnswers.TerminateAnswer + Environment.NewLine);
        }

        /// <summary>
        /// Determines whether n is  a prime number
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>
        ///   <c>true</c> if n [is prime number] then <c>true</c>; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPrimeNumber(int n)
        {
           if(n<=1)
            {
                return false;
            }else if (n <= 3)
            {
                return true;
            }else if (n % 2 == 0 || n % 3 == 0)
            {
                return false;
            }
            int i = 5;
            while (i * i <= n)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                {
                    return false;
                }
                i = i + 6;
            }
            return true;

        }
    }
}
