using System;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void DynamicThrowsRuntimeBinderExceptionForMissingMethods()
        {
            dynamic text = "hello dynamic";
            
            string works = text.Substring(6);
            Assert.Equal("dynamic", works);
            
            Assert.Throws<RuntimeBinderException>(() =>
            {
                string fails = text.SUBSTR(6);
            });
        }
        
        // The call is ambiguous between the following methods or properties: 'Examples.Examples.SimpleMethod(double)' and 'Examples.Examples.SimpleMethod(decimal)'
        // string SimpleMethod(double value) => "called double";
        string SimpleMethod(int value) => "called int";
        string SimpleMethod(decimal value) => "called decimal";
        string SimpleMethod(string value) => "called string";
        
        string RouteCall(dynamic value) => SimpleMethod(value);
        
        [Fact]
        public void DynamicMethodOverloadExamples()
        {
            Assert.Equal("called int", RouteCall(10));
            Assert.Equal("called decimal", RouteCall(10.5m));
            Assert.Equal("called decimal", RouteCall(10L));
            Assert.Equal("called string", RouteCall("ten"));
            
            Assert.Throws<RuntimeBinderException>(
                () => RouteCall(new object()));
            Assert.Throws<RuntimeBinderException>(
                // The best overloaded method match for 'Examples.Examples.SimpleMethod(int)' has some invalid arguments
                () => RouteCall(10.5));
        }
        
        /*
        “The compiler has to specify any default values for parameters, those values are embedded in the IL for the calling code.
        There’s no way for the compiler to say “I don’t have a value for this parameter; please use whatever default you have.”
        That’s why the default values have to be compile-time constants”
        -- Excerpt From: Jon Skeet. “C# in Depth, Fourth Edition.”
        */
        
        [Fact]
        public void ParametersWithDaultValuesAndNamedArgumentsExamples()
        {
            string Example(int x, int y = 5, int z = 8) => $"x={x} y={y} z={z}";
            
            Assert.Equal("x=1 y=2 z=3", Example(1, 2, 3));
            Assert.Equal("x=1 y=5 z=8", Example(1));
            Assert.Equal("x=10 y=9 z=8", Example(x:10, y:9, z:8));
            Assert.Equal("x=10 y=9 z=8", Example(z:8, x:10, y:9));
            // error CS1744: Named argument 'x' specifies a parameter for which a positional argument has already been given
            // Assert.Equal("will not compile", Example(43, x:10, y:9));
            Assert.Equal("x=10 y=5 z=8", Example(z:8, x:10));
            Assert.Equal("x=10 y=5 z=8", Example(10, z:8));
            var value = 1;
            Assert.Equal("x=1 y=3 z=2", Example(value++, z:value++, y:value++));
            
            string AnotherExample(int x = 1, params int[] xs) => $"x={x} xs={string.Join(", ", xs)}";
            
            Assert.Equal("x=1 xs=2, 3, 4", AnotherExample(1, 2, 3, 4));
            Assert.Equal("x=2 xs=8, 9", AnotherExample(2, 8, 9));
            Assert.Equal("x=1 xs=", AnotherExample());
            Assert.Equal("x=8 xs=", AnotherExample(x:8));
            Assert.Equal("x=8 xs=", AnotherExample(8));
            Assert.Equal("x=1 xs=2, 3, 4", AnotherExample(x:1, 2, 3, 4));
            // error CS1744: Named argument 'x' specifies a parameter for which a positional argument has already been given
            // Assert.Equal("will not compile", AnotherExample(2, 3, 4, x:1));
        }
        
        [Fact]
        public void IEnumerableIsCovariant()
        {
            IEnumerable<string> strings = new List<string> { "hi", "there" };
            IEnumerable<object> objects = strings;            
            Assert.Equal(strings, objects);
        }
        
        [Fact]
        public void IListIsNotCovariant()
        {
            IList<string> strings = new List<string> { "no" };
            // error CS0266: Cannot implicitly convert type 'System.Collections.Generic.IList<string>' to 'System.Collections.Generic.IList<object>'.
            // IList<object> objects = strings;
            // error CS1503: Argument 1: cannot convert from 'object' to 'string'
            // strings.Add(new object());
            object obj = strings[0];
            Assert.Equal(obj, strings[0]);
        }
    }
}
