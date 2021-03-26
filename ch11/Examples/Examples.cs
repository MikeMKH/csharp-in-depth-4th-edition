using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void MinMaxTupleExample()
        {
            var extremes = MinMax(new [] {1, 2, 3});
            Assert.Equal(1, extremes.min);
            Assert.Equal(3, extremes.max);
            
            extremes = MinMax(Enumerable.Range(-8888, 2 * 8888 + 1));
            Assert.Equal(-8888, extremes.min);
            Assert.Equal(8888, extremes.max);
            
            (int min, int max) MinMax(IEnumerable<int> values)
              => values.Skip(1).Aggregate(
                  (min: values.First(), max: values.First()),
                  (m, value) =>
                    (m.min < value ? m.min : value,
                    m.max > value ? m.max : value));
        }
        
        [Fact]
        public void TupleExamples()
        {
            // tuple literal
            var t1 = (8, y: "hello");
            Assert.Equal(8, t1.Item1);
            Assert.Equal("hello", t1.y);
            
            // tuple type
            (int x, string) t2;
            t2 = t1;
            Assert.Equal(t1.Item1, t2.x);
            Assert.Equal(t1.y, t2.Item2);
        }
        
        [Fact]
        public void FibonacciExample()
        {
            Assert.Equal(
                new [] {0, 1, 1, 2, 3, 5, 8, 13, 21, 34},
                Fibonacci().Take(10));
            
            IEnumerable<int> Fibonacci()
            {
                var state = (current: 0, next: 1);
                while(true)
                {
                    yield return state.current;
                    state = (state.next, state.current + state.next);
                }
            }
        }
        
        [Fact]
        public void TypesOfTupleLiteralsExamples()
        {
            var tuple1 = (x: 10, 20);
            (int x, int) tuple2 = (x: 10, 20);
            Assert.IsType<(int x, int)>(tuple1);
            Assert.Equal(tuple1, tuple2);
            
            var array1 = new [] {("a", 10)};
            (string, int) [] array2 = new [] {("a", 10)};
            Assert.IsType<(string, int)[]>(array1);
            Assert.Equal(array1, array2);
        }
        
        [Fact]
        public void ConversionsFromTupleLiteralsExamples()
        {
            (byte, object) t1 = (8, "tuple");
            
            Assert.Equal(8, t1.Item1);
            
            int x = Byte.MaxValue + 1;
            var t2 = ((byte, string)) (x, "tuple");
            var t3 = ((byte) x, "tuple");
            
            Assert.NotEqual(Byte.MaxValue + 1, t2.Item1);
            Assert.Equal(0, t2.Item1);
            
            Assert.NotEqual(Byte.MaxValue + 1, t3.Item1);
            Assert.Equal(0, t3.Item1);
            
            var z = (x, "hello");
            (long, string) z1 = z;
            // (byte, string) z2 = z; // error CS0266: Cannot implicitly convert type '(int x, string)' to '(byte, string)'
            (byte, string) z3 = ((byte, string)) z;
            (object, object) z4 = z;
            // (string, string) z5 = ((string, string)) z; // error CS0030: Cannot convert type '(int x, string)' to '(string, string)'
        }
        
        interface ITupleExample
        {
            void Method((int x, string) tuple);
        }
        
        class TupleExample : ITupleExample
        {
            public void Method((int x, string) arg)
              => Console.WriteLine($"x={arg.x} Item2={arg.Item2}");
        }
        
        [Fact]
        public void TupleEqualityExamples()
        {
            var t1 = (x: "x", y: 'y', z: 3);
            var t2 = ("x", 'y', 3);
            var t3 = ("no a match", 'x', 9);
            var t4 = ("will not compile");
            
            Assert.True(t1 == t2);
            Assert.False(t1 != t2);
            Assert.Equal(t1, t2);
            
            Assert.False(t1 == t3);
            Assert.True(t1 != t3);
            Assert.NotEqual(t1, t3);
            
            // Assert.True(t1 != t4); // error CS0019: Operator '!=' cannot be applied to operands of type '(string x, char y, int z)' and 'string'
        }
        
        [Fact]
        public void TupleCompilerExample()
        {
            var t1 = new ValueTuple<int, string> (8, "hello");
            Assert.Equal(8, t1.Item1);
            Assert.Equal("hello", t1.Item2);
            
            var t2 = new ValueTuple<int, string>(t1.Item1, t1.Item2);
            t2.Item1 = 42;
            t2.Item2 = "everything";
            Assert.Equal(8, t1.Item1);
            Assert.Equal("hello", t1.Item2);
            Assert.Equal(42, t2.Item1);
            Assert.Equal("everything", t2.Item2);
            
            var t3 = new ValueTuple<byte, string>((byte) t1.Item1, t1.Item2);
            Assert.Equal(8, t3.Item1);
            Assert.Equal("hello", t3.Item2);
        }
        
        [Fact]
        public void TupleToStringExample()
        {
            var t = (x: (int?) null, y: 2, z: "three");
            Assert.Null(t.x);
            Assert.Equal("(, 2, three)", t.ToString());
        }
        
        [Fact]
        public void TupleOfSize8AndLargerExample()
        {
            var t8 = (1, 2, 3, 4, 5, 6, 7, 8);
            Assert.IsType<ValueTuple<int, int, int, int, int, int, int, ValueTuple<int>>>(t8);
            Assert.Equal(8, t8.Item8);

            var t10 = (1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.IsType<ValueTuple<int, int, int, int, int, int, int, ValueTuple<int, int, int>>>(t10);
            Assert.Equal(10, t10.Item10);
        }
        
        [Fact]
        public void TupleWithLinqExample()
        {
            var values = (new [] { 1, 2, 3, 4, 5, 6, 7, 8 }).Select((x, idx) => new { Index = idx, Value = x });
            var q = from xs in values where xs.Value % 2 == 0 select xs;
            
            var ts = (from xs in q select (xs.Value, Value2: xs.Value * 2)).ToArray();
            Assert.Equal(2, ts[0].Value);
            
            var vs = (from xs in q select new { xs.Value, Value2 = xs.Value * 2 }).ToArray();
            Assert.Equal(2, vs[0].Value);
            
            Assert.Equal(ts[1].Value2, vs[1].Value2);
        }
    }
}
