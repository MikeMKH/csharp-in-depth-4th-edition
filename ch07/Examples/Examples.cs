using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void ForLoopCapturesVariables()
        {
            var asserts = new List<Action>();
            for(int i = 0; i < 3; i++)
              asserts.Add(
                () => { Console.WriteLine($"for i={i}"); Assert.Equal(3, i); });
            foreach(var assertion in asserts)
              assertion();
        }
        
        [Fact]
        public void ForLoopCapturesVariablesWhichLeadsToIndexOutOfRangeException()
        {
            var values = new [] { 0, 1, 2 };
            var asserts = new List<Action>();
            for(int i = 0; i < 3; i++)
              asserts.Add(() =>
                {
                    Assert.Equal(3, i);
                    Assert.Throws<IndexOutOfRangeException>(() => values[i]);
                });
            foreach(var assertion in asserts)
              assertion();
        }
        
        [Fact]
        public void ForeachLoopDoesNotCapturesVariables()
        {
            var asserts = new List<Action>();
            var values = Enumerable.Range(0,3);
            foreach(var value in values)
              asserts.Add(
                () => { Console.WriteLine($"foreach value={value}"); Assert.True(value < 3); });
            foreach(var assertion in asserts)
              assertion();
        }
    }
}
