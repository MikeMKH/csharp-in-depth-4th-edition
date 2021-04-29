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
    }
}
