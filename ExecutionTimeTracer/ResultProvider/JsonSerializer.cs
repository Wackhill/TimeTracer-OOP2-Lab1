using Newtonsoft.Json;

namespace ExecutionTimeTracer.ResultProvider
{
    public class JsonSerializer: ISerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}