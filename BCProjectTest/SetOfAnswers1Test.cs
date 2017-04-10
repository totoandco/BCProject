using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BCProject.Answers;
using System.Text;

namespace BCProjectTest
{

    /// <summary>
    /// Test Class for the implementation of the SetOfAnswers interface used in the server
    /// </summary>
    [TestClass]
    public class SetOfAnswers1Test
    {
        /// <summary>
        /// The set of answers
        /// </summary>
        public SetOfAnswers1 setOfAnswers = new SetOfAnswers1();
        /// <summary>
        /// Test of the ConnectionsCommandAnswer function
        /// </summary>
        [TestMethod]
        public void ConnectionsCommandAnswerTest()
        {

        CollectionAssert.AreEqual(Encoding.ASCII.GetBytes(3 + Environment.NewLine), setOfAnswers.ConnectionsCommandAnswer(3));

        }
        /// <summary>
        ///  Test of the CountCommandAnswer function
        /// </summary>
        [TestMethod]
        public void CountCommandAnswerTest()
        {

            CollectionAssert.AreEqual(Encoding.ASCII.GetBytes(3 + Environment.NewLine), setOfAnswers.CountCommandAnswer(3));

        }
        /// <summary>
        /// Test of the HeloCommandAnswer function
        /// </summary>
        [TestMethod]
        public void HeloCommandAnswerTest()
        {

            CollectionAssert.AreEqual(Encoding.ASCII.GetBytes("HI" + Environment.NewLine), setOfAnswers.HeloCommandAnswer());

        }
        /// <summary>
        /// Test of the TerminateCommandAnswer function
        /// </summary>
        [TestMethod]
        public void TerminateCommandAnswerTest()
        {

            CollectionAssert.AreEqual(Encoding.ASCII.GetBytes("BYE" + Environment.NewLine), setOfAnswers.TerminateCommandAnswer());

        }
        /// <summary>
        /// Test of the IsPrimeNumber function
        /// </summary>
        [TestMethod]
        public void IsPrimeNumberTest()
        {

            Assert.AreEqual(true, setOfAnswers.IsPrimeNumber(3));
            Assert.AreEqual(true, setOfAnswers.IsPrimeNumber(1429));
            Assert.AreEqual(true, setOfAnswers.IsPrimeNumber(1759));
            Assert.AreEqual(false, setOfAnswers.IsPrimeNumber(8));
            Assert.AreEqual(false, setOfAnswers.IsPrimeNumber(1572));
            Assert.AreEqual(false, setOfAnswers.IsPrimeNumber(2468));
            Assert.AreEqual(true, setOfAnswers.IsPrimeNumber(11311));
            Assert.AreEqual(true, setOfAnswers.IsPrimeNumber(26731));
            Assert.AreEqual(true, setOfAnswers.IsPrimeNumber(44729));
            Assert.AreEqual(true, setOfAnswers.IsPrimeNumber(104711));
        }
    }
    
}
