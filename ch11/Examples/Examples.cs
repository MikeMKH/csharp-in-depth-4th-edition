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
    }
}
