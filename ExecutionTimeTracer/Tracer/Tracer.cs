using System.Diagnostics;
using System.Reflection;
using System.Threading;
using ExecutionTimeTracer.DataStore;

namespace ExecutionTimeTracer.Tracer
{
    public class Tracer : ITracer
    {
        private bool IsResultReady { get; set; }
        private readonly TraceResult _result;

        public Tracer()
        {
            _result = new TraceResult();
        }

        public void StartTrace()
        {
            IsResultReady = false;
            int threadId = Thread.CurrentThread.ManagedThreadId;
            
            ThreadStatItem threadStat = _result.ThreadsStat.GetOrAdd(threadId, 
                _ => new ThreadStatItem(threadId));

            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            MethodStatItem methodStatItem = 
                new MethodStatItem(method.Name, method.ReflectedType?.Name);
            
            methodStatItem.StartTrace();
            
            if (threadStat.LastStackMethods.Count != 0)
            {
                threadStat.LastStackMethods.Peek().AddChildMethod(methodStatItem);
            }
            else
            {
                threadStat.Methods.Add(methodStatItem);
            }
            
            threadStat.LastStackMethods.Push(methodStatItem);
        }

        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            MethodStatItem methodStat = _result.ThreadsStat[threadId].LastStackMethods.Pop();
            methodStat.StopTrace();
        }

        public TraceResult GetTraceResult()
        {
            if (IsResultReady) return _result;
            GetThreadsActiveTime();
            IsResultReady = true;
            return _result;
        }
        
        private void GetThreadsActiveTime()
        {
            foreach (var thread in _result.ThreadsStat)
            {
                thread.Value.GetThreadActiveTime();
            }
        }
    }
}