using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int worker = 0;
            int threads = 0;
            ThreadPool.GetAvailableThreads(out worker, out threads);

            Console.WriteLine("Thread pool threads available at startup: ");
            Console.WriteLine("   Worker threads: {0:N0}", worker);
            Console.WriteLine("   Asynchronous I/O threads: {0:N0}", threads);

            var list = new List<int>();

            for (int i = 10000; i >= 0; i--)
            {
                list.Add(i);
            }

            //list = MergeSort(list);
            list = ParallelMergeSort(list, threads);

            Console.WriteLine(string.Join(", ", list));
        }

        private static List<int> ParallelMergeSort(List<int> listToSort, int threads)
        {
            if (threads <= 1)
            {
                return MergeSort(listToSort);
            }

            var left = new List<int>();
            var right = new List<int>();

            int middleIndex = listToSort.Count / 2;

            for (int i = 0; i < middleIndex; i++)
            {
                left.Add(listToSort[i]);
            }

            for (int i = middleIndex; i < listToSort.Count; i++)
            {
                right.Add(listToSort[i]);
            }

            var thread1 = new Thread(() =>
                left = ParallelMergeSort(left, threads/2));
            var thread2 = new Thread(() =>
                right = ParallelMergeSort(right, threads / 2));

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            return Merge(left, right);
        }

        private static List<int> MergeSort(List<int> listToSort)
        {
            if (listToSort.Count <= 1)
            {
                return listToSort;
            }

            var left = new List<int>();
            var right = new List<int>();

            int middleIndex = listToSort.Count / 2;

            for (int i = 0; i < middleIndex; i++)
            {
                left.Add(listToSort[i]);
            }

            for (int i = middleIndex; i < listToSort.Count; i++)
            {
                right.Add(listToSort[i]);
            }

            //left = MergeSort(left);
            //right = MergeSort(right);

            var thread1 = new Thread(() =>
                left = MergeSort(left));
            var thread2 = new Thread(() =>
            right = MergeSort(right));

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            return Merge(left, right);
        }

        private static List<int> Merge(List<int> left, List<int> right)
        {
            var result = new List<int>();

            while (left.Count > 0 && right.Count > 0)
            {
                if (left[0] <= right[0])
                {
                    result.Add(left[0]);
                    left.RemoveAt(0);
                }
                else
                {
                    result.Add(right[0]);
                    right.RemoveAt(0);
                }
            }

            while (left.Count > 0)
            {
                result.Add(left[0]);
                left.RemoveAt(0);
            }

            while (right.Count > 0)
            {
                result.Add(right[0]);
                right.RemoveAt(0);
            }

            return result;
        }
    }
}
