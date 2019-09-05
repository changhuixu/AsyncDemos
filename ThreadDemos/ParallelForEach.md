https://stackoverflow.com/questions/39794498/net-core-parallel-foreach-issues/39796934#39796934
Why Parallel.ForEach is not good for this task is explained in comments: it's designed for CPU-bound (CPU-intensive) tasks. If you use it for IO-bound operations (like making web requests) - you will waste thread pool thread blocked while waiting for response, for nothing good. It's possible to use it still, but it's not best for this scenario.

What you need is to use asynchronous web request methods (like HttpWebRequest.GetResponseAsync), but here comes another problem - you don't want to execute all your web requests at once (like another answer suggests). There may be thousands urls (ids) in your list. So you can use thread synchronization constructs designed for that, for example Semaphore. Semaphore is like queue - it allows X threads to pass, and the rest should wait until one of busy threads will finish it's work (a bit simplified description). Here is an example:



https://stackoverflow.com/questions/19102966/parallel-foreach-vs-task-run-and-task-whenall

In this case, the second method will asynchronously wait for the tasks to complete instead of blocking.

However, there is a disadvantage to use Task.Run in a loop- With Parallel.ForEach, there is a Partitioner which gets created to avoid making more tasks than necessary. Task.Run will always make a single task per item (since you're doing this), but the Parallel class batches work so you create fewer tasks than total work items. This can provide significantly better overall performance, especially if the loop body has a small amount of work per item.

If this is the case, you can combine both options by writing:
