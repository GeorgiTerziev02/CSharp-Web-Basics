using System;
using System.Diagnostics;
using System.Threading;

namespace Asynchronous_Processing
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            
            var lockObj = new object();
            decimal money = 0;
            int count = 0;

            var thread1 = new Thread(()=>
            {
                for (int i = 1; i <= 500000; i++)
                {
                    bool isPrime = true;

                    for (int j = 2; j <= Math.Sqrt(i); j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                        }
                    }

                    if (isPrime)
                    {
                        lock (lockObj)
                        {
                            count++;
                        }
                    }
                }
            });
            thread1.Start();

            var thread2 = new Thread(()=>
            {
                for (int i = 500001; i <= 1000000; i++)
                {
                    bool isPrime = true;

                    for (int j = 2; j <= Math.Sqrt(i); j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                        }
                    }

                    if (isPrime)
                    {
                        lock (lockObj)
                        {
                            count++;
                        }
                    }
                }
            });
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine(count);
            Console.WriteLine(sw.Elapsed);
        }

        private static void MyThreadMainMethod()
        {
            var sw = Stopwatch.StartNew();
            //Console.WriteLine(CountPrimeNumbers(1, 1000000));

            Console.WriteLine(sw.Elapsed);
        }
    }
}
