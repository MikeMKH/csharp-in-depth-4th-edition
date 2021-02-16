using System;
using System.Net; // WebClient
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public async void HttpClientExampleAsync()
        {
            var client = new HttpClient();
            var site = await client.GetStringAsync("https://www.instagram.com/mikemkh8/?hl=en");
            Assert.True(site.Length > 0);
        }
        
        [Fact]
        public void WebClientExample()
        {
            var client = new WebClient();
            // not async
            var site = client.DownloadString("https://racket-lang.org/");
            Assert.True(site.Length > 0);
        }
        
        [Fact]
        public async void AsynchronousBoundariesExample()
        {
            var client = new HttpClient();
            
            Task<int> length = GetPageLengthAsync("https://www.haskell.org/");
            Assert.True(length.Result > 0);
            
            async Task<int> GetPageLengthAsync(string url)
            {
                Task<string> result = client.GetStringAsync(url);
                return (await result).Length;
            }
        }
        
        [Fact]
        public void AsyncIsNotPartOfMethodSignatureExample()
        {
            PrintLengthOfPage("https://httpstat.us/200?sleep=301");
            PrintLengthOfPage("https://httpstat.us/200?sleep=102");
            PrintLengthOfPage("https://httpstat.us/200?sleep=3");
            PrintLengthOfPage("http://www.google.com");
            
            async void PrintLengthOfPage(string url)
            {
                var client = new HttpClient();
                var length = (await client.GetStringAsync(url)).Length;
                Console.WriteLine($"{url} has length {length}");
            }
            /*
            https://httpstat.us/200?sleep=3 has length 0
            https://httpstat.us/200?sleep=102 has length 0
            https://httpstat.us/200?sleep=301 has length 0
            */
        }
        
        [Fact]
        public async void WrappingOfReturnValuesExample()
        {
            var client = new HttpClient();
            
            Task<int> length = GetPageLengthAsync("https://dotnet.microsoft.com/");
            Assert.True(length.Result > 0);
            
            async Task<int> GetPageLengthAsync(string url)
            {
                Task<string> result = client.GetStringAsync(url);
                int length = (await result).Length;
                return length;
            }
        }
        
        [Fact]
        public void AwaitingCompleteAndUncompletedTasksExample()
        {
            Task t = ExampleAsync();
            Console.WriteLine(
                $"{Thread.CurrentThread.ManagedThreadId}: (1) Method return");
            t.Wait();
            Console.WriteLine(
                $"{Thread.CurrentThread.ManagedThreadId}: (2) Task complete");
            
            async Task ExampleAsync()
            {
                Console.WriteLine(
                    $"{Thread.CurrentThread.ManagedThreadId}: (3) Before first await");
                await Task.FromResult(8);
                Console.WriteLine(
                    $"{Thread.CurrentThread.ManagedThreadId}: (4) Between awaits");
                await Task.Delay(100);
                Console.WriteLine(
                    $"{Thread.CurrentThread.ManagedThreadId}: (5) After awaits");
            }
            /*
            18: (3) Before first await
            18: (4) Between awaits
            18: (1) Method return
            19: (5) After awaits
            18: (2) Task complete
            */
        }
        
        [Fact]
        public void AwaitedCodeThrowsAggregateException()
        {
            Assert.ThrowsAsync<AggregateException>(async () => await Fails());
            
            async Task Fails() => throw new ArgumentException("you cannot win");
        }
        
        [Fact]
        public void CanThrowExceptionBeforeAsync()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await Fails());
            
            Task<int> Fails()
            {
                throw new ArgumentException("does not wrap in AggregateException");
                
                return DoStuff();
                
                async Task<int> DoStuff()
                {
                    await Task.Delay(10);
                    return 8;
                }
            }
        }
        
        [Fact]
        public void AsyncLambdaStartAsSoonAsCalled()
        {
            Func<int, Task<int>> f = async x =>
            {
                Console.WriteLine(
                    $"{Thread.CurrentThread.ManagedThreadId}: (a) Starting {x}");
                await Task.Delay(x * 100);
                Console.WriteLine(
                    $"{Thread.CurrentThread.ManagedThreadId}: (b) Finished {x}");
                return x;
            };
            
            Console.WriteLine(
                    $"{Thread.CurrentThread.ManagedThreadId}: (1) Begin......");
            Task<int> first = f(10);
            Task<int> second = f(2);
            Console.WriteLine(
                    $"{Thread.CurrentThread.ManagedThreadId}: (2) End........");
            
            Assert.Equal(10, first.Result);
            Assert.Equal(2, second.Result);
            
            /*
            16: (1) Begin......
            16: (a) Starting 10
            16: (a) Starting 2
            16: (2) End........
            17: (b) Finished 2
            14: (b) Finished 10
            */
        }
        
        [Fact]
        public void ValueTaskExample()
        {
            var value = 42;
            var awaiter = Foo().GetAwaiter();
            if (!awaiter.IsCompleted) Assert.True(false, "was not complete");
            
            var result = awaiter.GetResult();
            Assert.Equal(value, result);
            
            ValueTask<int> Foo() => new ValueTask<int>(value);
        }
        
        [Fact]
        public void ValueTaskUsingDifferentPathExample()
        {
            var even = Foo(10);
            Assert.Equal(false, even.IsCompleted);
            var odd = Foo(11);
            Assert.Equal(true, odd.IsCompleted);
            
            // https://github.com/dotnet/runtime/issues/15809#issuecomment-160658188
            async ValueTask<int> Foo(int x)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: starting x={x}");
                if (x % 2 == 0)
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: processing x={x}");
                    await Task.Delay(x * 10);
                }
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: returning x={x}");
                return x;
            }
        }
    }
}
