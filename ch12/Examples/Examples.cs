using System;
using System.Text;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void DeconstructionOfTuplesExamples()
        {
            var t = (8, "hi");
            
            var (a, b) = t;
            
            (int c, string d) = t;
            
            int e;
            string f;
            (e, f) = t;
            
            Assert.Equal(t.Item1, a);
            Assert.Equal(t.Item1, c);
            Assert.Equal(t.Item1, e);
            
            Assert.Equal(t.Item2, b);
            Assert.Equal(t.Item2, d);
            Assert.Equal(t.Item2, f);
        }
        
        [Fact]
        public void DeclarationLooksLikeDeconstruction()
        {
            var t = ("hi", 007);
            (string s, int i) = t;
            (string s, int i) x = t;
            
            Assert.Equal(t.Item1, s);
            Assert.Equal(t.Item1, x.s);
            
            Assert.Equal(t.Item2, i);
            Assert.Equal(t.Item2, x.i);
        }
        
        [Fact]
        public void DiscardExamples()
        {
            (int x, _, _) = Foo();
            (_, var y, _) = Foo();
            (_, _, double z) = Foo();
            
            var r = Foo();
            
            Assert.Equal(r.a, x);
            Assert.Equal(r.b, y);
            Assert.Equal(r.c, z);
            
            (int a, string b, double c) Foo() => (1, "two", 3.0);
        }
        
        [Fact]
        public void EvaluationOrderMattersForDeconstruction()
        {
            var builder = new StringBuilder("abcdefg");
            var original = builder;
            
            Assert.Equal("abcdefg", builder.ToString());
            Assert.Equal("abcdefg", original.ToString());
            Assert.Equal(builder.Length, original.Length);
            
            (builder, builder.Length) = (new StringBuilder("1234567"), 3);
            
            Assert.Equal("1234567", builder.ToString());
            Assert.Equal("abc", original.ToString());
            Assert.NotEqual(builder.Length, original.Length);
        }
        
        [Fact]
        public void LiteralExamples()
        {
            (string text, Func<int, int> func) = ("example", x => x * 3 + 1);
            (text, func) = ("different", y => y * 7 - 2);
            Assert.Equal("different", text);
            Assert.Equal(12, func(2));
            
            (byte a, byte b) = (5, 42);
            Assert.Equal(5, a);
            Assert.Equal(42, b);
        }
        
        [Fact]
        public void ExtensionMethodDeconstructExample()
        {
            var (y1, m1, d1) = DateTime.Parse("2020-03-17"); // first day of working from home due to the pandemic
            Assert.Equal(2020, y1);
            Assert.Equal(3, m1);
            Assert.Equal(17, d1);
            
            (int y2, _, int d2) = DateTime.Parse("2005-10-07"); // wedding
            Assert.Equal(2005, y2);
            Assert.Equal(7, d2);
            
            var (_, _, _, h3, m3, s3) = DateTime.Parse("2020-04-28T12:02:20"); // jack ;(
            Assert.Equal(12, h3);
            Assert.Equal(2, m3);
            Assert.Equal(20, s3);
        }
        
        [Fact]
        public void ConstantPatternExamples()
        {
            Assert.Equal(1, Match("hello"));
            Assert.Equal(-1, Match("good bye"));
            Assert.Equal(10, Match(5L));
            Assert.Equal(-1, Match(7));
            Assert.Equal(100, Match(10));
            Assert.Equal(-1, Match(10L));
            
            int Match(object input)
            {
                if (input is "hello") return 1;
                if (input is 5L) return 10;
                if (input is 10) return 100;
                return -1;
            }
        }
        
        [Fact]
        public void TypePatternExamples()
        {
            Assert.False(IsType<int?>(null));
            Assert.True(IsType<int?>(5));
            Assert.False(IsType<int?>("five"));
            Assert.False(IsType<string?>(null));
            Assert.False(IsType<string?>(5));
            Assert.True(IsType<string?>("five"));
            
            bool IsType<T>(object value)
              => value is T;
        }
        
        [Fact]
        public void VarPatternExample()
        {
            Assert.Contains("int", Match(8));
            Assert.Contains("long", Match(42L));
            Assert.Contains("string", Match("eight"));
            Assert.Contains("DateTime", Match(DateTime.UtcNow));
            
            string Match(object input)
            {
                switch (input)
                {
                    case int i:
                      return $"int value {i}";
                    case long l:
                      return $"long value {l}";
                    case string s:
                      return $"string value {s}";
                    case var value:
                      return $"type of {value.GetType()}";
                }
            }
        }
        
        [Fact]
        public void IsOperatorExamples()
        {
            Assert.Equal("string=bar", Foo("bar"));
            Assert.Equal("builder=meh", Foo(new StringBuilder("meh")));
            Assert.Equal("unknown", Foo(8));
            
            var x1 = 8;
            if (x1 is int x1_ && x1_ > 0)
            {
                Assert.Equal(8, x1_);
            }
            Assert.Equal(8, x1_);
            
            var x2 = (long) x1;
            if (x2 is long x2_ && x2_ > 0)
            {
                Assert.Equal(8, x2_);
            }
            Assert.Equal(8, x2_);
            
            string Foo(object input)
            {
                if (input is string text)
                {
                    text = $"string={text}";
                }
                else if (input is StringBuilder builder)
                {
                    text = $"builder={builder.ToString()}";
                }
                else
                {
                    text = "unknown";
                }
                return text;
            }
        }
        
        [Fact]
        public void FibonacciSwitchPatternMatchExample()
        {
            Assert.Equal(0, Fibonacci(0));
            Assert.Equal(1, Fibonacci(1));
            Assert.Equal(1, Fibonacci(2));
            Assert.Equal(2, Fibonacci(3));
            Assert.Equal(3, Fibonacci(4));
            Assert.Equal(5, Fibonacci(5));
            Assert.Equal(8, Fibonacci(6));
            Assert.Equal(6765, Fibonacci(20));
            Assert.Equal(121393, Fibonacci(26));
            
            var ex = Record.Exception(() => Fibonacci(-1));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            
            int Fibonacci(int n, int a = 0, int b = 1)
            {
                switch (n)
                {
                    case 0: return a;
                    case 1: return b;
                    case var _ when n > 0: return Fibonacci(n-1, b, a + b);
                    default: throw new ArgumentOutOfRangeException("n must be non-negative value");
                }
            }
        }
        
        [Fact]
        public void SwitchGuardExample()
        {
            Assert.True(Check(126));
            Assert.True(Check(8L));
            Assert.False(Check(256));
            Assert.False(Check(1024L));
            Assert.False(Check(-1));
            Assert.False(Check(-42L));
            
            bool Check(object n)
            {
                switch (n)
                {
                    case int x when x > 255:
                    case long y when y > 255L:
                      return false;
                    case int x when x < 0:
                    case long y when y < 0L:
                      return false;
                    default: return true;
                }
            }
        }
        
        [Fact]
        public void FizzBuzzExample()
        {
            Assert.Equal("Fizz", FizzBuzz(3));
            Assert.Equal("Fizz", FizzBuzz(33));
            Assert.Equal("Buzz", FizzBuzz(5));
            Assert.Equal("Buzz", FizzBuzz(55));
            Assert.Equal("FizzBuzz", FizzBuzz(15));
            Assert.Equal("FizzBuzz", FizzBuzz(1515));
            Assert.Equal("2", FizzBuzz(2));
            Assert.Equal("22", FizzBuzz(22));
            
            string FizzBuzz(int n)
            {
                switch ((fizz: n % 3 == 0, buzz: n % 5 == 0))
                {
                    case (true, true): return "FizzBuzz";
                    case (true, false): return "Fizz";
                    case (false, true): return "Buzz";
                    default: return n.ToString();
                }
            }
        }
        
        [Fact]
        public void FizzBuzzGuardExample()
        {
            Assert.Equal("Fizz", FizzBuzz(3));
            Assert.Equal("Fizz", FizzBuzz(33));
            Assert.Equal("Buzz", FizzBuzz(5));
            Assert.Equal("Buzz", FizzBuzz(55));
            Assert.Equal("FizzBuzz", FizzBuzz(15));
            Assert.Equal("FizzBuzz", FizzBuzz(1515));
            Assert.Equal("2", FizzBuzz(2));
            Assert.Equal("22", FizzBuzz(22));
            
            string FizzBuzz(int n)
            {
                switch (n)
                {
                    case var _ when n % 3 == 0 && n % 5 == 0: return "FizzBuzz";
                    case var _ when n % 3 == 0 && n % 5 != 0: return "Fizz";
                    case var _ when n % 3 != 0 && n % 5 == 0: return "Buzz";
                    default: return n.ToString();
                }
            }
        }
    }
    
    public static class ExamplesExt
    {
        public static void Deconstruct(this DateTime value, out int year, out int month, out int day)
          => (year, month, day) = (value.Year, value.Month, value.Day);
        
        public static void Deconstruct(this DateTime value,
          out int year, out int month, out int day,
          out int hour, out int minute, out int second)
          => (year, month, day, hour, minute, second)
            = (value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
    }
}
