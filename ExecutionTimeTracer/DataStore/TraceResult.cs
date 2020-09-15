namespace ExecutionTimeTracer.DataStore
{
    public class TraceResult
    {
        public ThreadStatStore<int, ThreadStatItem> ThreadsStat { get; }

        public TraceResult()
        {
            ThreadsStat = new ThreadStatStore<int, ThreadStatItem>();
        }
        
        /*
        public int GetMethodsActiveTime(int threadId)
        {
            if (ThreadsStat.IsEmpty || !ThreadsStat.Keys.Contains(threadId))
            {
                return -1;
            }

            double result = 0;
            ThreadStatItem threadStatItem = ThreadsStat[threadId];

            foreach (MethodStatItem method in threadStatItem.Methods)
            {
                result += Math.Round(method.ActiveTime + GetInnerMethodsActiveTime(method.ChildMethods));
            }

            return (int)Math.Truncate(result);
        }
        */

        /*
        private double GetInnerMethodsActiveTime(List<MethodStatItem> methods)
        {
            if (methods == null)
            {
                return 0;
            }

            double activeTime = 0;

            foreach (MethodStatItem methodStatItem in methods)
            {
                activeTime += Math.Round(methodStatItem.ActiveTime + 
                                         GetInnerMethodsActiveTime(methodStatItem.ChildMethods));
            }

            return activeTime;
        }
        */
    }
}