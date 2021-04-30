using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void NullableReferenceTypeExamples()
        {
            string notNullable = "hello";
            string? nullable = default;
            
            Assert.Equal("hello", notNullable);
            Assert.Null(nullable);
            
            Assert.True(notNullable.Length > 0);
            
            var nullReference = Record.Exception(
                // () => nullable.Length > 0); // warning CS8602: Dereference of a possibly null reference.
                () => nullable!.Length > 0);
            Assert.IsType<NullReferenceException>(nullReference);
            
            var isNullExample = Record.Exception(() =>
                {
                    if (nullable is null)
                      return true;
                    else
                      return nullable.Length > 0;
                });
            Assert.Null(isNullExample);
            
            var needsHelpExample = Record.Exception(() =>
                {
                    if (string.IsNullOrEmpty(nullable))
                      return true;
                    else
                    //   return nullable.Length > 0; // warning CS8602: Dereference of a possibly null reference.
                      return nullable is null ? false : nullable.Length > 0;
                });
            Assert.Null(needsHelpExample);
            
            var methodExample = Record.Exception(() =>
                {
                    if (!IsValid(nullable))
                      return true;
                    else
                      return nullable.Length > 0;
                    
                    bool IsValid([NotNullWhen(true)] string? s)
                      => !string.IsNullOrEmpty(s);
                });
            Assert.Null(methodExample);
        }
        
        [Theory]
        [InlineData(0, "FizzBuzz")]
        [InlineData(1, "1")]
        [InlineData(3, "Fizz")]
        [InlineData(33, "Fizz")]
        [InlineData(5, "Buzz")]
        [InlineData(55, "Buzz")]
        [InlineData(15, "FizzBuzz")]
        [InlineData(45, "FizzBuzz")]
        [InlineData(7, "7")]
        [InlineData(77, "77")]
        public void FizzBuzzSwitchExpression(int value, string expected)
        {
            var actual = FizzBuzz(value);
            Assert.Equal(expected, actual);
            
            var whenActual = FizzBuzz_WithWhen(value);
            Assert.Equal(expected, whenActual);
            
            string FizzBuzz(int n)
              => (n % 3, n % 5) switch
              {
                  (0, 0) => "FizzBuzz",
                  (0, _) => "Fizz",
                  (_, 0) => "Buzz",
                  _ => n.ToString()
              };
              
            string FizzBuzz_WithWhen(int n)
              => n switch
              {
                  int m when (m % 3 == 0) && (m % 5 == 0) => "FizzBuzz",
                  int m when (m % 3 == 0) && (m % 5 != 0) => "Fizz",
                  int m when (m % 3 != 0) && (m % 5 == 0) => "Buzz",
                  _ => n.ToString()  
              };
        }
        
        [Fact]
        public void PatternMatchingExamples()
        {
            var hello = "hello";
            Func<string> helloFunc = () => "hello";
            var five = 5;
            
            // type patterns
            Assert.True(hello is string);
            Assert.True(helloFunc is Func<string>);
            Assert.True(five is int);
            
            // constant patterns
            Assert.True(hello is "hello");
            Assert.True(helloFunc() is "hello");
            Assert.True(five is 5);
            
            // var patterns
            Assert.True(hello is var s);
            Assert.Equal(hello, s);
            Assert.True(helloFunc() is var h);
            Assert.Equal(helloFunc(), h);
            Assert.True(five is var x);
            Assert.Equal(five, x);
        }
        
        public record Name(string First, string Last);
        public record Customer(Name Name, int Number);
        
        [Fact]
        public void PropertyPatternMatchingExamples()
        {
            var x = new Customer(Name: new Name("Mike", "Harris"), Number: 5);
            var y = new Customer(Name: new Name("Kelsey", "Harris"), Number: 4);
            var z = new Customer(Name: new Name("Jack", "Harris"), Number: 3);
            
            Assert.Equal("Harris", SirName(y));
            Assert.Equal("Harris", SirName(z));
            
            Assert.Equal("MikeMikeMikeMikeMike", RepeatFirstName(x));
            Assert.Equal("JackJackJack", RepeatFirstName(z));
            
            string SirName(Customer customer)
              => customer switch
              {
                  Customer { Name: { Last: var sirName } } => sirName,
                  _ => "unknown"
              };
            
            string RepeatFirstName(Customer customer)
              => customer switch
              {
                  Customer { Name: { First: var first }, Number: var times }
                    => string.Concat(System.Linq.Enumerable.Repeat(first, times)),
                  _ => ""
              };
        }
    }
}
