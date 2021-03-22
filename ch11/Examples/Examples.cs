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
    }
}
