using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReadTextFile.Tests
{
    [TestClass]
    public class ReadWordsTests
    {
        [TestMethod]
        public void ReadWords_MultiLineInput_ReturnsCorrectCount()
        {
            var fileReader = new ReadTextFile.FileReader();
            string text = "Hello world\nThis is a test";
            int count = fileReader.ReadWords(text);
            Assert.AreEqual(6, count);
        }
    }
}
