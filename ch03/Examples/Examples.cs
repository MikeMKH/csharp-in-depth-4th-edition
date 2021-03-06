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
        
        [Fact]
        public void AnonymousTypesExamples()
        {
            var player1 = new
            {
                Name = "Mike",
                Score = 8_000_000
            };
            
            Assert.Equal("Mike", player1.Name);
            Assert.Equal(8_000_000, player1.Score);
            
            var player2 = new
            {
                Name = "Kelsey",
                Score = 9_000_000
            };
            
            Assert.Equal(
                player1.GetType(), player2.GetType());
            
            player1 = new
            {
                Name = "Jack",
                Score = 200
            };
            
            Assert.Equal("Jack", player1.Name);
            Assert.Equal(200, player1.Score);
            
            var x = new
            {
                player1.Name,
                player2.Score,
                Age = 40,
                Ba = new Ba
                {
                    Prop = 8
                }
            };
            
            Assert.Equal(player1.Name, x.Name);
            Assert.Equal(player2.Score, x.Score);
            Assert.Equal(40, x.Age);
            Assert.Equal(8, x.Ba.Prop);
            
            var players = new []
            {
                new { Name = "Mac", Score = 7 },
                new { Name = "Pete", Score = 2 },
                new { Name = "Mark", Score = 9 }
            };
            
            Assert.Equal(
                player1.GetType(), players[0].GetType());
            Assert.Equal("Pete", players[1].Name);
        }
        
        [Fact]
        public void LambdaCaptureVariablesExample()
        {
            string instanceField = "instanceField";
            
            IDictionary<string, string> CreateAction(string methodParameter)
            {
                var results = new Dictionary<string, string>();
                string methodLocal = "methodLocal";
                string uncaptureLocal = "uncaptureLocal";
                string changedMethodLocal = "unchangedMethodLocal";
                
                Action<string> action = lambdaParameter =>
                {
                    string lambdaLocal = "lambdaLocal";
                    string changedLambdaLocal = "unchangedLambdaLocal";
                    
                    results.Add("methodParameter", methodParameter);
                    results.Add("methodLocal", methodLocal);
                    results.Add("uncaptureLocal", uncaptureLocal);
                    results.Add("lambdaParameter", lambdaParameter);
                    results.Add("lambdaLocal", lambdaLocal);
                    results.Add("instanceField", instanceField);
                    
                    changedMethodLocal = "changedMethodLocal";
                    results.Add("changedMethodLocal", changedMethodLocal);
                    
                    changedLambdaLocal = "changedLambdaLocal";
                    results.Add("changedLambdaLocal", changedLambdaLocal);
                };
                
                action("lambdaParameter");
                return results;
            }
            
            var r1 = CreateAction("methodParameter");
            Assert.Equal("methodParameter", r1["methodParameter"]);
            Assert.Equal("methodLocal", r1["methodLocal"]);
            Assert.Equal("uncaptureLocal", r1["uncaptureLocal"]);
            Assert.Equal("lambdaParameter", r1["lambdaParameter"]);
            Assert.Equal("lambdaLocal", r1["lambdaLocal"]);
            Assert.Equal("instanceField", r1["instanceField"]);
            Assert.Equal("changedMethodLocal", r1["changedMethodLocal"]);
            Assert.Equal("changedLambdaLocal", r1["changedLambdaLocal"]);
            
            instanceField = "changed";
            var r2 = CreateAction("changed");
            Assert.Equal("changed", r2["instanceField"]);
            Assert.Equal("instanceField", r1["instanceField"]);
            
            Assert.Equal("methodParameter", r1["methodParameter"]);
            Assert.Equal("methodLocal", r1["methodLocal"]);
            Assert.Equal("uncaptureLocal", r1["uncaptureLocal"]);
            Assert.Equal("lambdaParameter", r1["lambdaParameter"]);
            Assert.Equal("lambdaLocal", r1["lambdaLocal"]);
            Assert.Equal("instanceField", r1["instanceField"]);
            Assert.Equal("changedMethodLocal", r1["changedMethodLocal"]);
            Assert.Equal("changedLambdaLocal", r1["changedLambdaLocal"]);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(-2)]
        [InlineData(0)]
        [InlineData(200)]
        [InlineData(-99999)]
        public void ExtensionsTests(int a)
        {
            Assert.Equal(a + 3, a.Add(3));
            Assert.Equal(a - 3, a.Add(-3));
            Assert.Equal(a * 2, a.Add(a));
        }
        
        [Fact]
        public void ExtensionsWorkOnNullInstances()
        {
            Assert.Equal("!", ((string) null).SomethingThatTakesNullStrings('!'));
            
            string s = null;
            Assert.Equal("!", s.SomethingThatTakesNullStrings('!'));
            
            s = "Works with null";
            Assert.Equal("Works with null.", s.SomethingThatTakesNullStrings('.'));
            
            Assert.Equal("catz", "cat".SomethingThatTakesNullStrings('z'));
        }
        
        [Fact]
        public void ExtensionChainingExample()
        {
            var x = 
              9.Add(18).Add(27).Add(36).Add(45).Add(54).Add(63).Add(72).Add(81)
                .ToString()
                .SomethingThatTakesNullStrings('?');
            
            Assert.Equal("405?", x);
        }
        
        [Fact]
        public void QueryExpressionsAreSameAsMethodExpressions()
        {
            var words = new [] { "fee", "fi", "fo", "fum" };
            
            var x1 = words
              .Where(word => word.Length > 2)
              .OrderBy(word => word)
              .Select(word => word.ToUpper());
            
            var x2 = from word in words
              where word.Length > 2
              orderby word
              select word.ToUpper();
            
            Assert.Equal(x1, x2);
        }
        
        [Fact]
        public void TransparentIdentifierExample()
        {
            var words = new [] { "and", "the", "snakes", "start", "to", "sing" };
            
            var x1 = from word in words
              let length = word.Length
              where length > 2
              orderby length
              select $"{length}: {word.ToUpper()}";
            
            var x2 = words.Select(word => new { word, length = word.Length })
              .Where(w => w.length > 2)
              .OrderBy(w => w.length)
              .Select(w => $"{w.length}: {w.word.ToUpper()}");
            
            Assert.Equal(x1, x2);
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
    
    public static class ExampleExtensions
    {
        public static int Add(this int a, int b) => a + b;
        public static string SomethingThatTakesNullStrings(this string s, char c)
          => string.IsNullOrEmpty(s) ? new string(new [] { c }) : string.Concat(s, c);
    }
}
