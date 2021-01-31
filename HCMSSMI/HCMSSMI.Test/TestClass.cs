using Xunit;

namespace HCMSSMI.Test
{
    public class TestClass
    {
        
        [Theory]
        [InlineData("Hello World")]
        public void HelloWorld(string message)
        {
            var msg = message;

            Assert.Equal("Hello World", msg);

        }
    }
}
