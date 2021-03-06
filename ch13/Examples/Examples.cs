using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void SimpleRefExamples()
        {
            int x = 8;
            int y = x;
            Assert.Equal(x, y);
            
            IncrementAndDouble(ref x, ref x);
            Assert.Equal(18, x);
            Assert.Equal(8, y);
            
            void IncrementAndDouble(ref int p1, ref int p2)
            {
                p1++;
                p2 *= 2;
            }
        }
        
        [Fact]
        public void SimpleLocalRefExamples()
        {
            int x = 42;
            ref int y = ref x;
            x++;
            y++;
            Assert.Equal(44, x);
            
            var array = new (int x, int y)[3];
            for (int i = 0; i < 3; i++) array[i] = (i, i);
            for (int i = 0; i < 3; i++)
            {
                ref var element = ref array[i];
                element.x++;
                element.y *= 2;
            }
            Assert.Equal((3, 4), array[2]);
        }
        
        public int value;
        
        [Fact]
        public void LocalRefFieldExample()
        {
            var obj = new Examples();
            ref int x = ref obj.value;
            x = 10;
            Assert.Equal(10, obj.value);
            
            obj = new Examples();
            Assert.Equal(10, x);
            Assert.Equal(default(int), obj.value);
        }
        
        [Fact]
        public void LocalRefReassignExample()
        {
            int x = 10;
            int y = 20;
            ref int r = ref x;
            r++;
            r = ref y;
            r++;
            Assert.Equal(11, x);
            Assert.Equal(21, y);
        }
        
        [Fact]
        public void RefReturnExample()
        {
            int x = 10;
            ref int y = ref Identity(ref x);
            Assert.Equal(10, x);
            y++;
            Assert.Equal(11, x);
            
            (Identity(ref y))++;
            Assert.Equal(12, x);

            string s = "hi";
            ref string id = ref Identity(ref s);
            Assert.Equal("hi", s);
            id = id.ToUpper();
            Assert.Equal("HI", s);
            
            ref T Identity<T>(ref T p) => ref p;
        }
        
        [Fact]
        public void RefTernaryOperatorExample()
        {
            var values = Enumerable.Range(1, 100);
            var (even, odd) = CountEvenAndOdd(values);
            Assert.Equal(50, even);
            Assert.Equal(50, odd);
            
            (int even, int odd) CountEvenAndOdd(IEnumerable<int> values)
            {
                var result = (even: 0, odd: 0);
                foreach (var value in values)
                {
                    ref int counter =
                      ref (value & 1) == 0 ? ref result.even : ref result.odd;
                    counter++;
                }
                return result;
            }
        }
        
        private readonly int field = 42;
        ref readonly int GetFieldAlias() => ref field;
        
        [Fact]
        public void RefReadonlyExample()
        {
            ref readonly int x = ref GetFieldAlias();
            Assert.Equal(42, x);
            // (GetFieldAlias())++; // error CS8331: Cannot assign to method 'Examples.GetFieldAlias()' because it is a readonly variable
        }
        
        [Fact]
        public void InParameterExample()
        {
            DateTime date = DateTime.Parse("2020-04-28");
            var s1 = Format(date);
            var s2 = Format(in date);
            var s3 = Format(date.AddMinutes(1));
            // var s4 = Format(in date.AddMinutes(1)); // error CS8156: An expression cannot be used in this context because it may not be passed or returned by reference
            
            Assert.Equal("28/04/2020", s1);
            Assert.Equal("28/04/2020", s2);
            Assert.Equal("28/04/2020", s3);
            
            string Format(in DateTime value)
            {
                var text = value.ToString(
                    "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);
                return text;
            }
        }
        
        [Fact]
        public void InParameterStrangeExample()
        {
            const int VALUE = 8;
            int x = VALUE, y = x;
            
            Assert.Equal(VALUE, x);
            Assert.Equal(VALUE, y);
            
            InParameter(x, () => x++);
            ValueParameter(y, () => y++);
            
            Assert.Equal(VALUE + 1, x);
            Assert.Equal(VALUE + 1, y);
            
            void InParameter(in int p, Action action)
            {
                Assert.Equal(VALUE, p);
                action();
                Assert.Equal(VALUE + 1, p);
            }
            
            void ValueParameter(int p, Action action)
            {
                Assert.Equal(VALUE, p);
                action();
                Assert.Equal(VALUE, p); // strange
            }
        }
        
        struct ReadWriteYearMonthDay
        {
            public int Year { get; init; }
            public int Month { get; init; }
            public int Day { get; init; }
            
            public ReadWriteYearMonthDay(int year, int month, int day)
              => (Year, Month, Day) = (year, month, day);
        }
        
        readonly struct ReadOnlyYearMonthDay
        {
            public int Year { get; init; }
            public int Month { get; init; }
            public int Day { get; init; }
            
            public ReadOnlyYearMonthDay(int year, int month, int day)
              => (Year, Month, Day) = (year, month, day);
        }
        
        [Fact]
        public void ReadOnlyStructExample()
        {
            var r  = new ReadOnlyYearMonthDay(1984, 01, 15);
            var rw = new ReadWriteYearMonthDay(1984, 01, 15);
            
            Assert.Equal(ReadWriteFormat(rw), ReadOnlyFormat(r));
            
            string ReadWriteFormat(in ReadWriteYearMonthDay value)
              => $"{value.Year}-{value.Month}-{value.Day}";
              
            string ReadOnlyFormat(in ReadOnlyYearMonthDay value)
              => $"{value.Year}-{value.Month}-{value.Day}";
        }
        
        [Theory]
        [InlineData(1.0, 1.0)]
        [InlineData(0.1, 0.1)]
        [InlineData(-1.0, 1.0)]
        [InlineData(1.0, -1.0)]
        [InlineData(98.76, 12.34)]
        public void CanFindMagnitudeOfGivenPoint(double x, double y)
        {
            var p = new Point(x, y);
            var expected = Math.Sqrt(x * x + y * y);
            var actual = p.Magnitude();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void OffsetByChangesPoint()
        {
            var p = new Point(1.0, -1.0);
            Assert.Equal(new Point(1.0, -1.0), p);
            
            p.OffsetBy(new Point(-1.0, 1.0));
            Assert.Equal(new Point(0, 0), p);
        }
        
        [Fact]
        public void SpanExample()
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            var s = Generate(alphabet, random, 10);
            Assert.Equal(10, s.Length);
            
            string Generate(string alphabet, Random random, int length)
            {
                Span<char> chars = stackalloc char[length];
                for (int i = 0; i < length; i++)
                {
                    chars[i] = alphabet[random.Next(alphabet.Length)];
                }
                return new string(chars);
            }
        }
        
        [Fact]
        public void StringCreateExample()
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            var s = Generate(alphabet, random, 10);
            Assert.Equal(10, s.Length);
            
            string Generate(string alphabet, Random random, int length)
              => string.Create(length, (alphabet, random), (span, state) =>
              {
                 var a = state.alphabet;
                 var r = state.random;
                 for (int i = 0; i < span.Length; i++)
                 {
                     span[i] = a[r.Next(a.Length)];
                 }
              });
        }
    }
    
        public readonly struct Point
        {
            public double X { get; init; }
            public double Y { get; init; }
            public Point(double x, double y) => (X, Y) = (x, y);
        }
        
        public static class PointExt
        {
            public static double Magnitude(this in Point p)
              => Math.Sqrt(p.X * p.X + p.Y * p.Y);
            public static void OffsetBy(this ref Point original, in Point offset)
              => original = new Point(original.X + offset.X, original.Y + offset.Y);
        }
}
