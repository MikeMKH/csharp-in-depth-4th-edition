using System;
using System.Net; // WebClient
using System.Net.Http;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public async void HttpClientExampleAsync()
        {
            var client = new HttpClient();
            var site = await client.GetStringAsync("https://rancidrancid.com/");
            Assert.True(site.Length > 0);
        }
        
        [Fact]
        public void WebClientExample()
        {
            var client = new WebClient();
            // not async
            var site = client.DownloadString("https://rancidrancid.com/");
            Assert.True(site.Length > 0);
        }
    }
}
