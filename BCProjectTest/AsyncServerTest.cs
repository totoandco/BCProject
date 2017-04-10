using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BCProject.Domain;
using BCProject.Answers;
using System.Net.Sockets;
using System.Text;


namespace BCProjectTest
{
    /// <summary>
    /// Test Class for some function of the AsyncServer class
    /// </summary>
    [TestClass]
    public class AsyncServerTest
    {
        /// <summary>
        /// Test the initial set of answer after the AyncServer constructor
        /// </summary>
        [TestMethod]
        public void AsyncServerConstructorTest()
        {
            AsyncServer server = new AsyncServer();
            SetOfAnswers1 soa = new SetOfAnswers1();
            StringAssert.ReferenceEquals(soa.GetType().ToString(), server.SetOfAnswers.GetType().ToString());
           
        }

        /// <summary>
        /// Test the initial values after the ClientObject constructor
        /// </summary>
        [TestMethod]
        public void ClientObjectConstructorTest()
        {
           ClientObject c_object = new ClientObject();
            Assert.AreEqual(null, c_object.CSocket);
            Assert.AreEqual(false, c_object.IsHandshakeCompleted);
            Assert.AreEqual(false, c_object.IsSessionTerminated);

        }

        /// <summary>
        ///
        /// Test of the GetAnswer function
        /// </summary>
        [TestMethod]
        public void GetAnswerTest()
        {
            AsyncServer server = new AsyncServer();
            CollectionAssert.AreEqual(Encoding.ASCII.GetBytes("HI" + Environment.NewLine), server.GetAnswer("HELO",true));
            CollectionAssert.AreEqual(new byte[1024], server.GetAnswer("TERMINATE", false));
            CollectionAssert.AreEqual(Encoding.ASCII.GetBytes("0" + Environment.NewLine), server.GetAnswer("CONNECTIONS", true));
            CollectionAssert.AreEqual(Encoding.ASCII.GetBytes("0" + Environment.NewLine), server.GetAnswer("COUNT", true));
            CollectionAssert.AreEqual(Encoding.ASCII.GetBytes("BYE" + Environment.NewLine), server.GetAnswer("TERMINATE", true));

        }

        /// <summary>
        /// Test of the CleanSocketList function
        /// </summary>
        [TestMethod]
        public void CleanSocketListTest()
        {
            AsyncServer server = new AsyncServer();
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.ListSocket.Add(s);
            Assert.AreEqual(1, server.ListSocket.Count);
            Assert.AreEqual(0, server.CleanSocketList(server.ListSocket).Count);

        }


    }
}
