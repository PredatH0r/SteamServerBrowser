using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ServerBrowser
{
  class ThrottledThreadPool
  {
    private readonly BlockingCollection<Action> queue = new BlockingCollection<Action>();
    private readonly Semaphore semaphore;

    public ThrottledThreadPool(int concurrency = 10)
    {
      this.semaphore = new Semaphore(concurrency, concurrency);
      var thread = new Thread(this.TaskDispatcher);
      thread.Name = "XThreadPool Dispatcher";
      thread.IsBackground = true;
      thread.Start();
    }

    public void QueueUserWorkItem(WaitCallback handler, object state = null)
    {
      queue.Add(() => handler(state));
    }

    private void TaskDispatcher()
    {
      while (true)
      {
        var task = queue.Take();
        this.semaphore.WaitOne();
        ThreadPool.QueueUserWorkItem(state =>
        {
          try
          {
            task();
          }
          finally
          {
            this.semaphore.Release();
          }
        });
      }
    }
  }
}
