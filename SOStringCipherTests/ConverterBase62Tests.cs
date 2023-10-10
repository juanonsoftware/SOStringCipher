using SOStringCipher;
using System.Numerics;

namespace SOStringCipherTests
{
    [TestClass]
    public class ConverterBase62Tests
    {
        [TestMethod]
        public void CanConvertToBase62Test1()
        {
            var result = XConverter.ToBase62(BigInteger.Parse("5743988549353209482094823").ToByteArray());
            Assert.AreEqual("SiNhvjLw15gGql", result);
        }

        [TestMethod]
        public void CanConvertFromBase62Test1()
        {
            var result = XConverter.FromBase62Hash("SiNhvjLw15gGql");
            Assert.AreEqual("5743988549353209482094823", new BigInteger(result).ToString());
        }

        [TestMethod]
        public void CanConvertToBase62Test2()
        {
            var result = XConverter.ToBase62(BitConverter.GetBytes(62858496127267));
            Assert.AreEqual("HqerTqE7", result);
        }

        [TestMethod]
        public void CanConvertFromBase62Test2()
        {
            var result = XConverter.FromBase62Hash("HqerTqE7");
            Assert.AreEqual(62858496127267, new BigInteger(result));
        }
    }
}