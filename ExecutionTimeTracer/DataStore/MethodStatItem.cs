using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

namespace ExecutionTimeTracer.DataStore
{
    [Serializable]
    [XmlRoot("method")]
    public class MethodStatItem
    {
        [XmlIgnore]
        private Stopwatch _stopwatch;

        [XmlElement("methodName")]
        public string MethodName { get; set; }

        [XmlElement("time")]
        public long ActiveTime { get; set; }

        [XmlElement("className")]
        public string ClassName { get; set; }
        
        [XmlElement("methods")]
        public List<MethodStatItem> ChildMethods { get; set; }

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