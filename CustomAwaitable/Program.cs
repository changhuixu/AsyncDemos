using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CustomAwaitable
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now:O} ThreadID {Thread.CurrentThread.ManagedThreadId}: Begin await.");
            var result = await new CustomAwaitable();
            Console.WriteLine($"{DateTime.Now:O} ThreadID {Thread.CurrentThread.ManagedThreadId}: End await. Result is {result}.");
            await Task.Delay(Timeout.Infinite);
        }
    }

    internal class CustomAwaitable : INotifyCompletion
    {
        private readonly Task _task;

        public CustomAwaitable()
        {
            _task = Task.Delay(10000);
        }

        public void OnCompleted(Action continuation)
        {
            Console.WriteLine($"{DateTime.Now:O} ThreadID {Thread.CurrentThread.ManagedThreadId}: Invoke continuation action.");
            continuation?.Invoke();
        }

        public CustomAwaitable GetAwaiter() { return this; }
        public bool IsCompleted => _task.IsCompleted;
        public int GetResult()
        {
            Console.WriteLine($"{DateTime.Now:O} ThreadID {Thread.CurrentThread.ManagedThreadId}: Get result.");
            if (!IsCompleted)
            {
                throw new Exception();
            }
            return 123;
        }
    }
}
