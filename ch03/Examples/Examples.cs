using System;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void ImplicitlyTypedArraysExamples()
        {
            Assert.IsType<int []>(new int [] {1, 2, 3});
            Assert.IsType<int []>(new [] {1, 2, 3});
            Assert.IsType<int [,]>(new [,] {{1, 2, 3}, {4, 5, 6}});
            Assert.IsType<int [,]>(new [,] {{1, 2, 3}, {4, 5, 6}});
            Assert.IsType<string []>(new [] {"hello", null});
            Assert.IsType<object []>(new [] {"hello", new object()});

        }
    }
}
