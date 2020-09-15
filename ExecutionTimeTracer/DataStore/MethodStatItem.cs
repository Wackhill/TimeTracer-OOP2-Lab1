using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExecutionTimeTracer.DataStore
{
    public class MethodStatItem
    {
        private Stopwatch _stopwatch;

        public string MethodName { get; }

        public long ActiveTime { get; set; }

        public string ActiveTimeString
        {
            get
            {
                return Math.Round((double) ActiveTime) + "ms";
            }
            set
            {
                ActiveTimeString = value;
            }
        }

        public string ClassName;
        
        public List<MethodStatItem> ChildMethods { get; }

        public MethodStatItem() 
        {
            ChildMethods = new List<MethodStatItem>();
            _stopwatch = new Stopwatch();
        }

        public MethodStatItem(string methodName, string className): this()
        {
            MethodName = methodName;
            ClassName = className;
        }

        public void StartTrace()
        {
            _stopwatch.Start();
        }

        public void StopTrace()
        {
            _stopwatch.Stop();
            ActiveTime = _stopwatch.ElapsedMilliseconds;
        }

        public long GetActiveTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }

        public void AddChildMethod(MethodStatItem methodStatItem)
        {
            if (ChildExists(methodStatItem))
            {
                var listItem = ChildMethods.FirstOrDefault(item => item.MethodName == methodStatItem.MethodName);
                if (listItem != null) listItem.ActiveTime += methodStatItem.ActiveTime;
            }
            else
            {
                ChildMethods.Add(methodStatItem);   
            }
        }

        private bool ChildExists(MethodStatItem methodStatItem)
        {
            foreach (var method in ChildMethods)
            {
                if (method.MethodName.Equals(methodStatItem.MethodName))
                {
                    methodStatItem.ActiveTime += method.ActiveTime;
                    return true;
                }
            }

            return false;
        }
    }
}