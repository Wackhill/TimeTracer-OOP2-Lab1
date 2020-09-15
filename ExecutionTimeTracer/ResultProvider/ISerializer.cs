namespace ExecutionTimeTracer.ResultProvider
{
    public interface ISerializer
    {
        string Serialize(object obj);
    }
}