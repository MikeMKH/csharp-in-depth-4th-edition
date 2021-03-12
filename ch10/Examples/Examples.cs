using System;
using static System.String;
using static System.Linq.Queryable;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void StaticImportAllowsForUsageOfStaticMethods()
        {
            var values = new [] { "Hello", "World" };
            Assert.Equal("Hello World", Join(' ', values));
        }
        
        [Fact]
        public void SelectiveImportOfStaticExtensionMethods()
        {
            var values = new [] { 1, 2, 3 }.AsQueryable();
            Expression<Func<int, bool>> isEven = x => x % 2 == 0;
            Assert.Equal(new [] { 2 },  values.Where(isEven));
        }
        
        [Fact]
        public void FizzBuzzExample()
        {
            Func<int, string, Func<int, string>> test = (n, s) => x => x % n == 0 ? s : Empty;
            var tests = new Func<int, string>[] { test(3, "Fizz"), test(5, "Buzz") };
            Func<int, string> fizzbuzzer = x =>
            { 
                var result = Join(Empty, tests.Select(t => t(x)));
                return IsNullOrEmpty(result) ? x.ToString() : result;
            };
            
            Assert.Equal("2", fizzbuzzer(2));
            Assert.Equal("Fizz", fizzbuzzer(3));
            Assert.Equal("4", fizzbuzzer(4));
            Assert.Equal("Buzz", fizzbuzzer(5));
            Assert.Equal("Fizz", fizzbuzzer(6));
            Assert.Equal("Fizz", fizzbuzzer(9));
            Assert.Equal("FizzBuzz", fizzbuzzer(15));
            Assert.Equal("FizzBuzz", fizzbuzzer(45));
        }
    }
}
