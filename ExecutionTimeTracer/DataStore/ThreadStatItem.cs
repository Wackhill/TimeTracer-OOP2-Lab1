using System;
using System.Collections.Generic;

namespace ExecutionTimeTracer.DataStore
{
    /*
     * ThreadStruct
     *
     * Id
     * ExecutionTime
     * Methods[]
     */
    public class ThreadStatItem
    {
        public int Id { get; }
        public double ActiveTime { get; set; }
        
        public List<MethodStatItem> Methods { get; }

        public Stack<MethodStatItem> LastStackMethods { get; }

        public int MethodsNumber { get; private set; }

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