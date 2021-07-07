using Charitas.Processes.Implemetations;
using NUnit.Framework;

namespace Charitas.Tests
{
    public class Tests
    {
        private MersennePrimes _primes;

        [SetUp]
        public void Setup()
        {
            _primes = new MersennePrimes();
        }

        /// <summary>
        /// Tests the known mersenne primes.
        /// Reference: https://mathworld.wolfram.com/MersennePrime.html
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="expected">if set to <c>true</c> [expected].</param>
        [Test]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(5, true)]
        [TestCase(7, true)]
        [TestCase(13, true)]
        [TestCase(17, true)]
        [TestCase(19, true)]
        [TestCase(31, true)]
        [TestCase(61, true)]
        [TestCase(89, true)]
        [TestCase(107, true)]
        [TestCase(127, true)]
        [TestCase(521, true)]
        [TestCase(607, true)]
        [TestCase(1279, true)]
        [TestCase(2203, true)]
        [TestCase(2281, true)]
        [TestCase(3217, true)]
        [TestCase(4253, true)]
        [TestCase(4423, true)]
        [TestCase(9689, true)]
        [TestCase(9941, true)]
        [TestCase(11213, true)]
        [TestCase(19937, true)]
        [TestCase(21701, true)]
        [TestCase(23209, true)]
        [TestCase(44497, true)]
        [TestCase(86243, true)]
        [TestCase(110503, true)]
        [TestCase(132049, true)]
        [TestCase(216091, true)]
        [TestCase(756839, true)]
        [TestCase(859433, true)]
        [TestCase(1257787, true)]
        [TestCase(1398269, true)]
        [TestCase(2976221, true)]
        [TestCase(3021377, true)]
        [TestCase(6972593, true)]
        [TestCase(13466917, true)]
        [TestCase(20996011, true)]
        [TestCase(24036583, true)]
        [TestCase(25964951, true)]
        [TestCase(30402457, true)]
        [TestCase(32582657, true)]
        [TestCase(37156667, true)]
        [TestCase(42643801, true)]
        [TestCase(43112609, true)]
        [TestCase(57885161, true)]
        [TestCase(74207281, true)]
        [TestCase(77232917, true)]
        [TestCase(82589933, true)]
        public void TestKnownMersennePrimes(int input, bool expected)
        {
            Assert.AreEqual(_primes.IsMersennePrime(input), expected);
        }
    }
}