using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronous_Processing
{
    class Program
    {
        static async Task Exception()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://softuni.bg");
        }

        //public async Task<IActionResult> SaveData()
        //{
        //    //...
        //    //...
        //    await dbContext.SaveChangesAsyncs();
        //    //..
        //    return this.view();
        //}

        static async Task Main(string[] args)
        {
            //TASK PARALLEL LIBRARY (TPL)
            var httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync("https://softuni.bg");

            var result = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            //Task.Run(() =>
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        Console.WriteLine(i);
            //    }
            //}).ContinueWith((previousTask) =>
            //{
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        Console.WriteLine(i);
            //    }

            //});    

            //Task.Run(() =>
            //{
            //    for (int i = 300; i < 500; i++)
            //    {
            //        Console.WriteLine(i);
            //    }
            //});

            //while (true)
            //{
            //    var line = Console.ReadLine();
            //    Console.WriteLine(line.ToUpper());
            //}


            // CANT CATCH THREAD EXCEPTION
            //try
            //{
            //    var thread1 = new Thread(Exception);
            //    thread1.Start();
            //    thread1.Join();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}


            //Stopwatch sw = Stopwatch.StartNew();

            // DEAD LOCK
            //var lockObj1 = new object();
            //var lockObj2 = new object();

            //var thread1 = new Thread(() =>
            //{
            //    lock(lockObj1)
            //    {
            //        Thread.Sleep(1000);
            //        lock (lockObj2)
            //        {

            //        }
            //    }
            //});
            //var thread2 = new Thread(() =>
            //{
            //    lock (lockObj2)
            //    {
            //        Thread.Sleep(1000);
            //        lock (lockObj1)
            //        {

            //        }
            //    }
            //});

            //thread1.Start();
            //thread2.Start();
            //thread1.Join();
            //thread2.Join();

            //var numbers = new ConcurrentQueue<int>(Enumerable.Range(0, 10000).ToList());

            //for (int i = 0; i < 4; i++)
            //{
            //    new Thread(() =>
            //    {
            //        while(numbers.Count > 0)
            //        {
            //            numbers.TryDequeue(out _);
            //        }
            //    });
            //}

            //return;

            var lockObj = new object();
            int count = 0;
            var sw = Stopwatch.StartNew();

            //for (int i = 1; i <= 1000000; i++)
            Parallel.For(1, 1000001, (i) =>
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
         });

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
