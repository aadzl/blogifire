using System;
using Xunit;

namespace BlogiFire.Tests
{
    public class Test
    {
        [Fact]
        public void ThisIsTest()
        {
            Assert.Equal(BlogiFire.Core.Class1.Msg(), "from class library.");
        }

    }
}
