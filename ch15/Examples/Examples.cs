using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
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
        
        public bool WasDisposeCalled = false;
        
        public class AsynchronousDisposalClassExample
        {
            Examples caller;
            public AsynchronousDisposalClassExample(Examples examples) { caller = examples; }
            public async Task DisposeAsync() { caller.WasDisposeCalled = true; }
            public async Task<int> FiveAsync() => 5;
        }
        
        [Fact]
        public async void AsynchronousDisposalExample()
        {
            Assert.False(this.WasDisposeCalled);
            await using (var example = new AsynchronousDisposalClassExample(this))
            {
                  var five = await example.FiveAsync();
                  Assert.Equal(5, five);
            };
            Assert.True(this.WasDisposeCalled);   
        }
        
        [Fact]
        public async void AsynchronousStreamExample()
        {
            int sum = 0;
            await foreach (var value in Generate8())
            {
                Assert.Equal(8, value);
                sum += value;
            }
            Assert.Equal(8, sum);
            
            async IAsyncEnumerable<int> Generate8() { yield return 8; }
        }
        
        public interface IAdder
        {
            int Add(int a, int b) => a + b;
        }
        
        public class TypicalAdder : IAdder {}
        
        public class Plus1Adder : IAdder
        {
            public int Add (int a, int b) => 1 + a + b; // must be declared public, otherwise hidden
        }
        
        [Fact]
        public void DefaultInterfaceMethodExample()
        {
            IAdder x = new TypicalAdder();
            IAdder y = new Plus1Adder();
            
            Assert.Equal(4, x.Add(3, 1));
            Assert.Equal(5, y.Add(3, 1));
        }
        
        public record Person(string FirstName, string LastName);
        
        [Fact]
        public void RecordExample()
        {
            var mike = new Person("Mike", "Harris");
            Person kelsey = mike with { FirstName = "Kelsey" };
            Person bob = new("Bob", "Smith");
            
            // mike.FirstName = "John"; //  error CS8852: Init-only property or indexer 'Examples.Person.FirstName' can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor
            
            Assert.Equal(mike.LastName, kelsey.LastName);
            Assert.Equal("Mike", mike.FirstName);
            Assert.Equal(new ("Mike", "Harris"), mike);
            Assert.Equal(new Person ("Bob", "Smith"), bob);
        }
    }
}
