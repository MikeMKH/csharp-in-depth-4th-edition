using System;
using System.Linq;
using System.Globalization;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void StringsCanBeLeftAligned()
        {
            Assert.Equal("$123.45 ", $"{123.45,-8:C}");
            Assert.Equal("$123.45 ",
              string.Format(CultureInfo.GetCultureInfo("en-US"), $"{123.45,-8:C}"));
            Assert.Equal("$98.76  ", $"{98.76,-8:C}");
        }
        
        [Fact]
        public void StringsCanBeRightAligned()
        {
            Assert.Equal(" $123.45", $"{123.45,8:C}");
            Assert.Equal(" $123.45",
              string.Format(CultureInfo.GetCultureInfo("en-US"), "{0,8:C}", 123.45));
            Assert.Equal(" £123.45",
              string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0,8:C}", 123.45));
            Assert.Equal("123,45 ¤",
              string.Format(CultureInfo.GetCultureInfo("fr"), "{0,8:C}", 123.45));
            Assert.Equal("123,45 €",
              string.Format(CultureInfo.GetCultureInfo("fr-BE"), "{0,8:C}", 123.45));
            Assert.Equal("  $98.76", $"{98.76,8:C}");
        }
        
        [Fact]
        public void DateExample()
        {
            var date = System.DateTime.Now;
            CultureInfo.GetCultures(CultureTypes.AllCultures)
              .Take(5)
              .ToList().ForEach(c => Console.WriteLine(string.Format(c, "{0,-15} {1,12:d}", c.Name, date)));
        }
        
        [Fact]
        public void StringFormatIsSimilarToStringInterpolation()
        {
            var x = 10;
            var y = 20;
            
            var s1 = $"x={x} y={y}";
            var s2 = string.Format("x={0} y={1}", x, y);
            
            Assert.Equal(s1, s2);
        }
    }
}
