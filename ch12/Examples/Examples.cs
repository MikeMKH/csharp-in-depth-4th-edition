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
    }
}
