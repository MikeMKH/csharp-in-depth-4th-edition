using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        
        [Fact]
        public void CallerFilePathCallerLineNumberCallerMemberNameCanBeProvidedByTheCompiler()
        {
            (string path, string member, int line) Identity(
                [CallerFilePath] string path = null,
                [CallerMemberName] string member = null,
                [CallerLineNumber] int line = -1) => (path, member, line);
            
            var (path, member, line) = Identity();
            Console.WriteLine($"{path} {member} {line}");
            Assert.NotNull(path);
            Assert.NotNull(member);
            Assert.NotEqual(-1, line); // should be 58
            
            (path, member, line) = Identity("fake.py", "hack-it", 1984);
            Console.WriteLine($"{path} {member} {line}");
            Assert.Equal("fake.py", path);
            Assert.Equal("hack-it", member);
            Assert.Equal(1984, line);
        }
        
        [Fact]
        public void DynamicTypesPreserveCallerInformation()
        {
            int CalledByLine(string value, [CallerLineNumber] int line = -1)
            {
                Console.WriteLine($"value={value} line={line}");
                return line;
            }
            
            dynamic value = "hello";
            var line1 = CalledByLine(value);
            var line2 = CalledByLine((string) value);
            Assert.NotEqual(-1, line1);
            Assert.NotEqual(-1, line2);
        }
    }
}
