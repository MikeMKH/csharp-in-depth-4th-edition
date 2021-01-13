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
}
