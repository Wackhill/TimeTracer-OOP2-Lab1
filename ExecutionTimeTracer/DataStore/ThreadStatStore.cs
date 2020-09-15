using System;
using System.Collections.Concurrent;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ExecutionTimeTracer.DataStore
{
    public class ThreadStatStore<K, V> : ConcurrentDictionary<K, V>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            Console.WriteLine("Not implemented");
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Console.WriteLine("Not implemented");
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(V));
            foreach (K key in Keys)
            {
                V value = this[key];
                xmlSerializer.Serialize(writer, value);
            }
        }
    }
}