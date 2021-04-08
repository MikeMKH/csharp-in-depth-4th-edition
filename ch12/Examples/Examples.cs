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
