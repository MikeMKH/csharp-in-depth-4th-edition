using System;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void LocalMethodWithLoopExample()
        {
            int result = 0;
            for (int i = 0; i < 10; i++)
            {
                result += Process();
                
                int Process() => 0; // local must be in for loop so that i is in-scope
            }
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void LocalMethodVaribleMustBeDeclaredBeBeingUsedExample()
        {
            // Process(ref p); // error CS0841: Cannot use local variable 'p' before it is declared
            int p = 0;
            Process(ref p);
            Assert.Equal(1, p);
            
            void Process(ref int x) => x++;
        }
    }
}
