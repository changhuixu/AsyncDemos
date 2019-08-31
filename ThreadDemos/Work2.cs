using System;
using System.Collections.Generic;
using System.Threading;

namespace ThreadDemos
{
    public class Work2
    {
        public static void DoWork()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{DateTime.Now:o} Thread[{Thread.CurrentThread.ManagedThreadId}]: Working thread...");
                Thread.Sleep(100);
            }
        }

        public static void Do()
        {
            var thread1 = new Thread(DoWork);
            thread1.Start();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{DateTime.Now:o} Thread[{Thread.CurrentThread.ManagedThreadId}]: In main.");
                Thread.Sleep(100);
            }
        }
    }
    // The example displays output like the following:
    //       In main.
    //       Working thread...
    //       In main.
    //       Working thread...
    //       In main.
    //       Working thread...
}
