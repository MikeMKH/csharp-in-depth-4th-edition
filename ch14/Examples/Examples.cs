using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void LocalMethodWithLoopExample()
        {
            int result = 0;
            for (int i = 0; i < 10; i++)
            {
                result += Process();
                
                int Process() => 0; // local must be in for loop so that i is in-scope
            }
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void LocalMethodVariableMustBeDeclaredBeBeingUsedExample()
        {
            // Process(ref p); // error CS0841: Cannot use local variable 'p' before it is declared
            int p = 0;
            Process(ref p);
            Assert.Equal(1, p);
            
            void Process(ref int x) => x++;
        }
        
        [Fact]
        public void OutVariableExample()
        {
            Assert.Equal(123, ParseInt32("123"));
            Assert.Equal(-456, ParseInt32("-456"));
            Assert.Null(ParseInt32("ok"));
            Assert.Null(ParseInt32("2021-04-22"));
            Assert.Null(ParseInt32("1.23456789"));
            
            Nullable<int> ParseInt32(string str) => Int32.TryParse(str, out int value) ? value : (int?) null;
        }
        
        [Fact]
        public void IntegerLiteralsExamples()
        {
            byte b1 = 0b1000_0001;
            byte b2 = 0x81;
            byte b3 = 129;
            Assert.Equal(b1, b2);
            Assert.Equal(b2, b3);
            
            int maxInt32 = 2_147_483_647;
            Assert.Equal(Int32.MaxValue, maxInt32);
            
            decimal oneMillionDollars = 1_000_000.00m;
            Assert.Equal(1_000.00m * 1_000.00m, oneMillionDollars);
        }
        
        [Fact]
        public async void DefaultArgumentExample()
        {
            var expected = "hello";
            var actual = await FetchValueAsync(expected);
            
            Assert.Equal(expected, actual);
            
            async Task<string> FetchValueAsync(string value, CancellationToken cancellationToken = default)
              => value;
        }
        
        [Fact]
        public void DefaultLiteralExamples()
        {
            var a1 = new [] {default, 1};
            var a2 = new [] {default, "hello"};
            Assert.Equal(default(int), a1[0]);
            Assert.Equal(default, a1[0]);
            Assert.Equal(default(string), a2[0]);
            Assert.Equal(default, a2[0]);
            
            var p1 = DefaultTo10();
            var p2 = DefaultTo10(7);
            var p3 = DefaultTo10(default);
            Assert.Equal(10, p1);
            Assert.Equal(7, p2);
            Assert.Equal(0, p3);
            Assert.Equal(default, p3);
            
            int DefaultTo10(int n = 10) => n;
        }
        
        [Fact]
        public void NamedArgumentExamples()
        {
            var t1 = TupleIdentity(1, z: 2, y: 3);
            Assert.Equal((1, 3, 2), t1);
        
            var t2 = TupleIdentity(1, y: 2, 3);
            Assert.Equal((1, 2, 3), t2);
        
            var t3 = TupleIdentity(1, 2, 3);
            Assert.Equal((1, 2, 3), t3);
            
            (int a, int b, int c) TupleIdentity(int x, int y, int z) => (x, y, z);
        }
        
        enum ExampleEnum {}
        
        [Fact]
        public void GenericTypeConstraintsExamples()
        {
            Assert.Equal("Enum", EnumMethod<ExampleEnum>());
            // Assert.Equal("Enum", EnumMethod<Enum>()); // error CS0453: The type 'Enum' must be a non-nullable value type in order to use it as parameter 'T' in the generic type or method 'EnumMethod<T>()'
            
            Assert.Equal("Delegate", DelegateMethod<Action>());
            Assert.Equal("Delegate", DelegateMethod<Delegate>());
            Assert.Equal("Delegate", DelegateMethod<MulticastDelegate>());
            
            Assert.Equal("Unmanaged", UnmanagedMethod<int>());
            Assert.Equal("Unmanaged", UnmanagedMethod<System.Int32>());
            Assert.Equal("Unmanaged", UnmanagedMethod<double>());
            Assert.Equal("Unmanaged", UnmanagedMethod<char>());
            // Assert.Equal("Unmanaged", UnmanagedMethod<String>()); // error CS8377: The type 'string' must be a non-nullable value type, along with all fields at any level of nesting, in order to use it as parameter 'T' in the generic type or method 'UnmanagedMethod<T>()'
            
            string EnumMethod<T>() where T : struct, Enum => "Enum";
            string DelegateMethod<T>() where T : Delegate => "Delegate";
            string UnmanagedMethod<T>() where T : unmanaged => "Unmanaged";
        }
    }
}
