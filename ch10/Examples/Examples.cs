using System;
using static System.String;
using static System.Linq.Queryable;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using Xunit;

namespace Examples
{
    public class Examples
    {
        [Fact]
        public void StaticImportAllowsForUsageOfStaticMethods()
        {
            var values = new [] { "Hello", "World" };
            Assert.Equal("Hello World", Join(' ', values));
        }
        
        [Fact]
        public void SelectiveImportOfStaticExtensionMethods()
        {
            var values = new [] { 1, 2, 3 }.AsQueryable();
            Expression<Func<int, bool>> isEven = x => x % 2 == 0;
            Assert.Equal(new [] { 2 },  values.Where(isEven));
        }
        
        [Fact]
        public void FizzBuzzExample()
        {
            Func<int, string, Func<int, string>> test = (n, s) => x => x % n == 0 ? s : Empty;
            var tests = new Func<int, string>[] { test(3, "Fizz"), test(5, "Buzz") };
            Func<int, string> fizzbuzzer = x =>
            { 
                var result = Join(Empty, tests.Select(t => t(x)));
                return IsNullOrEmpty(result) ? x.ToString() : result;
            };
            
            Assert.Equal("2", fizzbuzzer(2));
            Assert.Equal("Fizz", fizzbuzzer(3));
            Assert.Equal("4", fizzbuzzer(4));
            Assert.Equal("Buzz", fizzbuzzer(5));
            Assert.Equal("Fizz", fizzbuzzer(6));
            Assert.Equal("Fizz", fizzbuzzer(9));
            Assert.Equal("FizzBuzz", fizzbuzzer(15));
            Assert.Equal("FizzBuzz", fizzbuzzer(45));
        }
        
        [Fact]
        public void IndexersCanBeUsedInObjectInitializers()
        {
            var text = "This text is too long or something.";
            
            StringBuilder builder1 = new StringBuilder(text)
            {
                Length = 20,
            };
            builder1[19] = '\u2026';
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"text={builder1}");
            
            StringBuilder builder2 = new StringBuilder(text)
            {
                Length =20,
                [19] = '\u2026',
            };
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"text={builder2}");
            
            Assert.Equal(builder1.ToString(), builder2.ToString());
        }
        
        [Fact]
        public void ObjectInitializerThrowsArgumentExceptionForDuplicates()
        {
            var exception = Record.Exception(() => new Dictionary<string, int>
            {
                {"A", 1},
                {"B", 2},
                {"B", 3},
            });
            Assert.IsType<ArgumentException>(exception);
        }
        
        [Fact]
        public void IndexersInitializerDoesNotThrowsArgumentExceptionForDuplicates()
        {
            var exception = Record.Exception(() => new Dictionary<string, int>
            {
                ["A"] = 1,
                ["B"] = 2,
                ["B"] = 3,
            });
            Assert.Null(exception);
        }
        
        [Fact]
        public void NullableBooleanComparisonExamples()
        {
            string value;
            const string excepted = "nope";
            
            value = null;
            Assert.Equal(value?.Equals(excepted) ?? false, value?.Equals(excepted) == true);
            Assert.Equal(value?.Equals(excepted) ?? true, value?.Equals(excepted) != false);
            
            value = excepted;
            Assert.Equal(value?.Equals(excepted) ?? false, value?.Equals(excepted) == true);
            Assert.Equal(value?.Equals(excepted) ?? true, value?.Equals(excepted) != false);
        }
        
        [Fact]
        public void IndexersAndNullConditionalOperatorExamples()
        {
            int[] values = null;
            Assert.Null(values?[0]);
            
            values = new int[] {1, 2, 3};
            Assert.Throws<IndexOutOfRangeException>(() => values?[4]);
        }
        
        [Fact]
        public void FuncAndNullConditionalOperatorExample()
        {
            Func<int, int> f = null;
            Assert.Null(f?.Invoke(8));
        }
        
        static bool PrintMessageAndReturnValue(string message, bool result)
        {
            Console.WriteLine(message);
            return result;
        }
        
        static void A()
        {
            try 
            {
                Console.WriteLine("try: A");
                throw new Exception("A");
            }
            finally
            {
                Console.WriteLine("finally: A");
            }
        }
        
        static void B()
        {
            try
            {
                Console.WriteLine("try: B");
                A();
            }
            catch(Exception)
              when(PrintMessageAndReturnValue("when: B", false))
            {
                Console.WriteLine("catch: B");
            }
            finally
            {
                Console.WriteLine("finally: B");
            }
        }
        
        static void C()
        {
            try
            {
                Console.WriteLine("try: C");
                B();
            }
            catch(ArgumentException)
              when(PrintMessageAndReturnValue("when: C <ArgumentException>", true))
            {
                Console.WriteLine("no idea how we got here");
            }
            catch(Exception)
              when(PrintMessageAndReturnValue("when: C <Exception>", true))
            {
                Console.WriteLine("catch: C");
            }
            finally
            {
                Console.WriteLine("finally: C");
            }
        }
        
        [Fact]
        public void ExceptionFiltersExample()
        {
            var expection = Record.Exception(() => C());
            Assert.Null(expection);
        }
        /*
        try: C
        try: B
        try: A
        when: B
        when: C <Exception>
        finally: A
        finally: B
        catch: C
        finally: C
        */
        
        [Fact]
        public void RetryExample()
        {
            Func<int> f = () =>
            {
                throw new Exception("I've got problems");
                return 8;
            };
            
            var expection = Record.Exception(() => Retry(f, 3, 1));
            Assert.IsType<Exception>(expection);
            
            expection = Record.Exception(() => Retry(f, 1, 100));
            Assert.IsType<Exception>(expection);
            
            T Retry<T>(Func<T> operation, int attempts = 2, int millisecondsTimeout = 10)
            {
                while(true)
                {
                    try
                    {
                        attempts--;
                        operation();
                    }
                    catch(Exception e) when(attempts >= 0)
                    {
                        Console.WriteLine($"Retry: failed={e}");
                        Console.WriteLine($"Retry: attempts left={attempts}");
                        Thread.Sleep(millisecondsTimeout);
                    }
                }
            }
        }
        
        [Fact]
        public void LogExample()
        {
            Func<int> f = () => throw new Exception("BOOM!");
            
            var expection = Record.Exception(() =>
            {
              try
              {
                  f();
              }
              catch(Exception e) when(Log(e)){}
            });
            Assert.IsType<Exception>(expection);
            
            bool Log(Exception e)
            {
                Console.WriteLine($"{DateTime.UtcNow}: {e.GetType()} {e.Message}");
                return false;
            }
        }
    }
}
