using SOStringCipher;

namespace SOStringCipherTests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void CanConvertToBase36Test1()
        {
            var result = XConverter.ToBase36(BitConverter.GetBytes(100));
            Assert.AreEqual("2S", result.ToUpper());
        }

        [TestMethod]
        public void CanConvertToBase36Test2()
        {
            var result = XConverter.ToBase36(BitConverter.GetBytes(10110));
            Assert.AreEqual("7SU", result.ToUpper());
        }
    }
}