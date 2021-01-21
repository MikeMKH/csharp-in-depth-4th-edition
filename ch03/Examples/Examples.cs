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
        public void ImplicitlyTypedArraysExamples()
        {
            Assert.IsType<int []>(new int [] {1, 2, 3});
            Assert.IsType<int []>(new [] {1, 2, 3});
            Assert.IsType<int [,]>(new [,] {{1, 2, 3}, {4, 5, 6}});
            Assert.IsType<int [,]>(new [,] {{1, 2, 3}, {4, 5, 6}});
            Assert.IsType<string []>(new [] {"hello", null});
            Assert.IsType<object []>(new [] {"hello", new object()});

        }
        
        [Fact]
        public void ObjectInitializerExamples()
        {
            var sut = new A
            {
                PropString = "hello",
                PropBb = new Bb
                {
                    Prop = "hi"
                },
                Collection =
                {
                    new Ba { Prop = 1 },
                    new Ba { Prop = 2 }
                }
            };
            
            Assert.Equal("hello", sut.PropString);
            Assert.Equal("hi", sut.PropBb.Prop);
            Assert.Equal(1, sut.Collection[0].Prop);
            Assert.Equal(2, sut.Collection[1].Prop);
            
            var ba1 = new Ba();
            ba1.Prop = 1;
            var ba2 = new Ba { Prop = 1 };
            Assert.Equal(ba1.Prop, ba2.Prop);
        }
        
        [Fact]
        public void CollectionInitializerExamples()
        {
            var l1 = new List<int>() { 1, 2, 3 };
            var l2 = new List<int> { 1, 2, 3 };
            var l3 = new List<int>();
            l3.Add(1);
            l3.Add(2);
            l3.Add(3);
            
            Assert.Equal(l1, l2);
            Assert.Equal(l1, l3);
            Assert.Equal(l2, l3);
            
            var d1 = new Dictionary<string, int>() { { "A", 1 }, { "B", 2 } };
            var d2 = new Dictionary<string, int> { { "A", 1 }, { "B", 2 } };
            var d3 = new Dictionary<string, int>();
            d3.Add("A", 1);
            d3.Add("B", 2);
            
            Assert.Equal(d1, d2);
            Assert.Equal(d1, d3);
            Assert.Equal(d2, d3);
            
            var foo1 = new Foo { 1 };
            var foo2 = new Foo { { 2 }, { 3 } };
        }
    }
    
    public class A
    {
        private readonly List<Ba> collection = new List<Ba>();
        public String PropString { get; set; }
        public Bb PropBb { get; set; }
        
        public List<Ba> Collection { get { return collection; } }
    }
    
    public class Ba
    {
        public int Prop { get; set; }
    }
    
    public class Bb
    {
        public string Prop { get; set; }
    }
    
    public class Foo : IEnumerable
    {
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
        public Foo GetEnumerator() => throw new NotImplementedException();
        
        public void Add(int x) => Console.WriteLine($"x={x}");
    }
}
