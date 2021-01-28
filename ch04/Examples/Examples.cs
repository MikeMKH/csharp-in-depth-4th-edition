using System;
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
    }
}
