using ExecutionTimeTracer.DataStore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExecutionTimeTracer.ResultProvider
{
    public class CustomXmlSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            /*
            //DataContractSerializer dataContractSerializer;//= new DataContractSerializer();
            TraceResult traceResult = (TraceResult) obj;
            

            using (MemoryStream memStm = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(TraceResult));
                serializer.WriteObject(memStm, traceResult);

                memStm.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memStm))
                {
                    string result = streamReader.ReadToEnd();
                    return result;
                }
            }
            */
            TraceResult traceResult = (TraceResult)obj;

            var serializer = new XmlSerializer(typeof(TraceResult));
            StringWriter strWriter = null;
            try
            {
                strWriter = new StringWriter();
                serializer.Serialize(strWriter, obj);
            }
            finally
            {
                if (strWriter != null)
                {
                    strWriter.Dispose();
                }
            }
            return strWriter.ToString();
        }
    }
}