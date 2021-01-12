using System;
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
    }
    
    class GenericCounter<T>
    {
        private static int value;
        
        static GenericCounter()
          => Console.WriteLine($"GenericCounter initialized for {typeof(T)}");
        
        public static int Increment() => ++value;
        
        public static int Decrement() => --value;
        
    }
}
