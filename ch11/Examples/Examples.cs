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
    }
}
