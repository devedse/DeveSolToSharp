using DeveSolToSharp.Helpers;
using Xunit;

namespace DeveSolToSharp.Tests.Helpers
{
    public class StringHelperFacts
    {
        [Fact]
        public void FirstCharToUpperReturnsFirstCharToUpper()
        {
            //Arrange
            var input = "test";

            //Act
            var result = StringHelper.FirstCharToUpper(input);

            //Assert
            Assert.Equal("Test", result);
        }
    }
}
