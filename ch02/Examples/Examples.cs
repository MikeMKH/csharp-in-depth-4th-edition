using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void GenericCounterExample()
        {
            Assert.Equal(1, GenericCounter<int>.Increment());
            Assert.Equal(2, GenericCounter<int>.Increment());
            Assert.Equal(1, GenericCounter<int>.Decrement());
            
            Assert.Equal(1, GenericCounter<string>.Increment());
            Assert.Equal(2, GenericCounter<string>.Increment());
            
            Assert.Equal(0, GenericCounter<int>.Decrement());
            
            Assert.Equal(3, GenericCounter<string>.Increment());
            
            // same type as int
            Assert.Equal(-1, GenericCounter<Int32>.Decrement());
            
            // different type than int
            Assert.Equal(1, GenericCounter<Int16>.Increment());
        }
        
        [Fact]
        public void NullableTypeSetToNullGetTypeThrowsNullReferenceExpection()
        {
            var sut = new Nullable<int>();
            Assert.Throws<NullReferenceException>(() => sut.GetType());
            
            var sut2 = new int?();
            Assert.Throws<NullReferenceException>(() => sut2.GetType());
        }
        
        [Fact]
        public void NullableTypeSetToValueIsBoxedToValueType()
        {
            var sut = new Nullable<int>(8);
            Assert.IsType<int>(sut);
            Assert.Equal(typeof(int), sut.GetType());
            
            var sut2 = new int?(8);
            Assert.IsType<int>(sut2);
            Assert.Equal(typeof(int), sut2.GetType());
        }
        
        [Fact]
        public void NullCoalescingOperatorExamples()
        {
            var none = new int?();
            var five = 5;
            var maybeFive = new int?(5);
            
            Assert.IsType<int>(none ?? five);
            Assert.IsType<int>(maybeFive ?? five);
            Assert.IsType<int>(maybeFive ?? none);
            // Assert.IsType<int?>(maybeFive ?? none); // Actual:   System.Int32
            Assert.IsType<int>(none ?? maybeFive);
        }
        
        delegate int Value(string value);
        int Five(object value) => 5;
        int Six(string value) => 6;
        Int32 Seven(String value) => 7;
        Int16 NotSameByIdentityConversion8(string value) => 8;
        Int64 NotSameByIdentityConversion9(string value) => 9;
        
        [Fact]
        public void DelegateCompatibilityExamples()
        {
            var f5 = new Value(Five);
            var f6 = new Value(Six);
            var f7 = new Value(Seven);
            // var f8 = new Value(NotSameByIdentityConversion8); // 'short has the wrong return type
            // var f9 = new Value(NotSameByIdentityConversion9); // 'long  has the wrong return type
            
            Assert.Equal(f5("must be a string"), 5);
            Assert.Equal(Five(new {}), 5);
            
            Assert.Equal(f6("a string"), 6);
            Assert.Equal(f7("a String"), 7);
        }
        
        int finallyCounter = 0;
        IEnumerable<int> SimpleIterator()
        {
            try
            {
                yield return 10;
                for(int i = 2; i < 5; i++)
                {
                    yield return 10 * i;
                }
                yield return 50;
            }
            finally
            {
                finallyCounter++;
            }
        } 
        
        [Fact]
        public void IteratorExample()
        {
            int counter = 1;
            foreach(var value in SimpleIterator())
            {
                Assert.Equal(counter * 10, value);
                counter++;
            }
            Assert.Equal(1, finallyCounter);
        }
        
        // 2.4.4
        IEnumerable<int> Fibonacci()
        {
            int current = 1;
            int next = 1;
            while(true)
            {
                yield return current;
                
                int t = current;
                current = next;
                next = next + t;
            }
        }
        
        [Fact]
        public void FibonacciExample()
        {
            Assert.Equal(1, Fibonacci().ElementAt(0));
            Assert.Equal(1, Fibonacci().ElementAt(1));
            Assert.Equal(2, Fibonacci().ElementAt(2));
            Assert.Equal(3, Fibonacci().ElementAt(3));
            Assert.Equal(5, Fibonacci().ElementAt(4));
            
            Assert.Equal(89, Fibonacci().ElementAt(10));
            Assert.Equal(46368, Fibonacci().ElementAt(23));
        }
        
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 1, 2)]
        [InlineData(5, 4, 9)]
        [InlineData(5, 9, 14)]
        [InlineData(5, 9900, 9905)]
        // [InlineData(5, 99900, 99905)] // Test host process crashed : Stack overflow.
        public void PartialExampleTests(int x1, int x2, int expected)
          => Assert.Equal(expected, PartialExample.Adder(x1, x2));
        
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, -1)]
        [InlineData(1, 1, 0)]
        [InlineData(5, 4, 1)]
        [InlineData(5, 9, -4)]
        public void MorePartialExampleTests(int x1, int x2, int expected)
          => Assert.Equal(expected, (new PartialExample()).Subtractor(x1, x2));
    }
    
    // 2.1.7
    class GenericCounter<T>
    {
        private static int value;
        
        static GenericCounter()
          => Console.WriteLine($"GenericCounter initialized for {typeof(T)}");
        
        public static int Increment() => ++value;
        
        public static int Decrement() => --value;
        
    }
    
    partial class PartialExample
    {
        public static int Adder(int a, int b)
          => ActualAdder(a, b);
        
        public int Subtractor(int a, int b)
        {
            int result = 0;
            FastSubtractor(ref result, a, b);
            return result;
        }
        
        partial void FastSubtractor(ref int result, int x, int y);
        partial void SlowSubtractor(ref int result, int x, int y);
    }
    
    partial class PartialExample
    {
        private static int ActualAdder(int x, int y)
          => y == 0 ? x : ActualAdder(x + 1, y - 1); // not a great idea since C# does not have tail call optimization
        
        partial void FastSubtractor(ref int result, int x, int y)
          => result = x - y;
    }
}
