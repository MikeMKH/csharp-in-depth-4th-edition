using System;
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
    }
}
