using System;
using System.Net; // WebClient
using System.Net.Http;
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
        }
    }
}
