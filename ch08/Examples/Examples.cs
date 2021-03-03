using System;
using Xunit;

namespace Examples
{
    public class Examples
    {
        struct Point
        {
            public double X { get; }
            public double Y { get; }
            
            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
        
        [Fact]
        public void StructUsingAutomaticallyImplementedPropertiesExample()
        {
            var p1 = new Point(1, 2);
            var p2 = new Point(1 ,2);
            var p3 = new Point(-0.987, 12.345);
            
            Assert.Equal(p1, p1);
            Assert.Equal(p1.X, p2.X);
            Assert.Equal(p1.Y, p2.Y);
            Assert.Equal(p1, p2);
            Assert.Equal(p3, p3);
            Assert.NotEqual(p1, p3);
            Assert.NotEqual(p2, p3);
        }
    }
}
