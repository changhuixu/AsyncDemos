using System;
using System.Threading;

namespace ThreadDemos
{
    public class Work1
    {
        public static void Do()
        {
            // Start a thread that calls a parameterized static method.
            var newThread = new Thread(DoWorkWithData);
            newThread.Start(42);

            // Start a thread that calls a parameterized instance method.
            var w = new Work1();
            newThread = new Thread(w.DoMoreWork);
            newThread.Start("42");
        }

        public static void DoWorkWithData(object data)
        {
            Console.WriteLine($"{DateTime.Now:o} Thread[{Thread.CurrentThread.ManagedThreadId}]: Static thread procedure. Data=[{data}]");
        }

        public void DoMoreWork(object data)
        {
            Console.WriteLine($"{DateTime.Now:o} Thread[{Thread.CurrentThread.ManagedThreadId}]: Instance thread procedure. Data=[{data}]");
        }
    }
    // This example displays output like the following (the two output lines can have different order):
    // Thread[5]: Instance thread procedure.Data=[42]
    // Thread[4]: Static thread procedure.Data=[42]
}
