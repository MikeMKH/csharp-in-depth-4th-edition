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
    }
}
