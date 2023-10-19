using SOStringCipher;
using System.Numerics;

namespace SOStringCipherTests
{
    [TestClass]
    public class ConverterBase36Tests
    {
        [TestMethod]
        public void CanConvertToBase36Test1()
        {
            var result = XConverter.ToBase36(BitConverter.GetBytes(100));
            Assert.AreEqual("2S", result.ToUpper());
        }

        [TestMethod]
        public void CanConvertFromBase36Test1()
        {
            var result = XConverter.FromBase36Hash("2S");
            Assert.AreEqual(100, new BigInteger(result));
        }

        [TestMethod]
        public void CanConvertToBase36Test2()
        {
            var result = XConverter.ToBase36(BitConverter.GetBytes(10110));
            Assert.AreEqual("7SU", result.ToUpper());
        }

        [TestMethod]
        public void CanConvertFromBase36Test2()
        {
            var result = XConverter.FromBase36Hash("7SU");
            Assert.AreEqual(10110, new BigInteger(result));
        }

        [TestMethod]
        public void CanConvertToBase36Test3()
        {
            var result = XConverter.ToBase36(BitConverter.GetBytes(1011098));
            Assert.AreEqual("LO62", result.ToUpper());
        }

        [TestMethod]
        public void CanConvertFromBase36Test3()
        {
            var result = XConverter.FromBase36Hash("3L0X9RGB6");
            Assert.AreEqual(10110988949586, new BigInteger(result));
        }

        [TestMethod]
        public void CanConvertToBase36TestNegative()
        {
            var result = XConverter.ToBase36(BitConverter.GetBytes(-10000));
            Assert.AreEqual("-7PS", result.ToUpper());
        }

        [TestMethod]
        public void CanConvertFromBase36TestNegative()
        {
            var result = XConverter.FromBase36Hash("-7PS");
            Assert.AreEqual(-10000, new BigInteger(result));
        }
    }
}