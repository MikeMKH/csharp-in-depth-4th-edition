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
              .ToList().ForEach(c => 
                {
                  FormattableString s = $"{c.Name,-15} {date,12:d}";
                  Console.WriteLine(s.ToString(c));
                });
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
        
        [Fact]
        public void InterpolatedStringsAreNotDynamiclyFormatted()
        {
            var value = "before";
            var s1 =  $"value={value}";
            Assert.Equal("value=before", s1);
            value = "after";
            Assert.Equal("value=before", s1);
            Assert.Equal("after", value);
            
            value = "before";
            FormattableString s2 = $"value={value}";
            Assert.Equal("value=before", s2.ToString());
            value = "after";
            Assert.Equal("value=before", s2.ToString());
            Assert.Equal("after", value);
        }
        
        [Fact]
        public void InterpolatedStringsUsesExpressions()
        {
          var value = 42;
          Assert.Equal(42, value);
          var _ = $"{((Func<int>)(() => value++))()}";
          Assert.Equal(43, value);
        }
    }
}
