using System;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void SimpleRefExamples()
        {
            int x = 8;
            int y = x;
            Assert.Equal(x, y);
            
            IncrementAndDouble(ref x, ref x);
            Assert.Equal(18, x);
            Assert.Equal(8, y);
            
            void IncrementAndDouble(ref int p1, ref int p2)
            {
                p1++;
                p2 *= 2;
            }
        }
    }
}
