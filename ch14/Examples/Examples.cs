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
        public void LocalMethodVariableMustBeDeclaredBeBeingUsedExample()
        {
            // Process(ref p); // error CS0841: Cannot use local variable 'p' before it is declared
            int p = 0;
            Process(ref p);
            Assert.Equal(1, p);
            
            void Process(ref int x) => x++;
        }
        
        [Fact]
        public void OutVariableExample()
        {
            Assert.Equal(123, ParseInt32("123"));
            Assert.Equal(-456, ParseInt32("-456"));
            Assert.Null(ParseInt32("ok"));
            Assert.Null(ParseInt32("2021-04-22"));
            Assert.Null(ParseInt32("1.23456789"));
            
            Nullable<int> ParseInt32(string str) => Int32.TryParse(str, out int value) ? value : (int?) null;
        }
        
        [Fact]
        public void IntegerLiteralsExamples()
        {
            byte b1 = 0b1000_0001;
            byte b2 = 0x81;
            byte b3 = 129;
            Assert.Equal(b1, b2);
            Assert.Equal(b2, b3);
            
            int maxInt32 = 2_147_483_647;
            Assert.Equal(Int32.MaxValue, maxInt32);
            
            decimal oneMillionDollars = 1_000_000.00m;
            Assert.Equal(1_000.00m * 1_000.00m, oneMillionDollars);
        }
    }
}
