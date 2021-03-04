using System;
using Xunit;

namespace Examples
{       
        
    public class Examples
    {
        public struct Point
        {
            public double X { get; }
            public double Y { get; }
            
            public Point(double x, double y) => (X, Y) = (x, y);
            
            public static Point Add(Point left, Point right) => left + right;
            public static Point operator+(Point left, Point right)
              => new Point(left.X + right.X, left.Y + right.Y);
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
        
        [Fact]
        public void OperationUsingExpressionBodyMethodExample()
        {
            var p1 = new Point(1, 2);
            var p2 = new Point(3, 4);
            Assert.Equal(new Point(4, 6), p1 + p2);
            Assert.Equal(Point.Add(p1, p2), p1 + p2);
        }
    }
}
