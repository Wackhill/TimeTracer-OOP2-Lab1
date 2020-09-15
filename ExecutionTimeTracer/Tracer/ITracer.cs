using ExecutionTimeTracer.DataStore;

namespace ExecutionTimeTracer.Tracer
{
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();
    }
}