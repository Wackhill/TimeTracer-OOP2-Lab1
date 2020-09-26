using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ExecutionTimeTracer.DataStore
{
    [Serializable]
    [XmlRoot("method")]
    public class ThreadStatItem
    {
        [XmlElement("id")]
        public int Id { get; set; }
        
        [XmlElement("time")]
        public double ActiveTime { get; set; }
        
        [XmlElement("methods")]
        public List<MethodStatItem> Methods { get; set; }

        [XmlIgnore]
        public Stack<MethodStatItem> LastStackMethods { get; set; }

        [XmlElement("methodsNumber")]
        public int MethodsNumber { get; set; }

        private ThreadStatItem()
        {
            LastStackMethods = new Stack<MethodStatItem>();
            Methods = new List<MethodStatItem>();
            Id = 0;
            ActiveTime = 0;
        }
        
        public ThreadStatItem(int id): this()
        {
            Id = id;
        }
        
        public double GetThreadActiveTime()
        {
            MethodsNumber = 0;
            ActiveTime = GetAllMethodsActiveTime(Methods);
            return ActiveTime;
        }

        private double GetAllMethodsActiveTime(List<MethodStatItem> methods)
        {
            double fullTime = 0;
            foreach (MethodStatItem methodStatItem in methods)
            {
                MethodsNumber++;
                fullTime += Math.Round(methodStatItem.GetActiveTime() +
                                       GetAllMethodsActiveTime(methodStatItem.ChildMethods));
            }

            ActiveTime = fullTime;
            return fullTime;
        }

        public override string ToString()
        {
            string result = "";
            foreach (var method in Methods)
            {
                result += method.MethodName + "\n";
            }

            return result;
        }
    }
}