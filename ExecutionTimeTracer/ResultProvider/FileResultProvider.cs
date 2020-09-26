using System.IO;

namespace ExecutionTimeTracer.ResultProvider
{
    public class FileResultProvider : IResultProvider
    {
        private string PathToSave { get; }

        public FileResultProvider(string filePath)
        {
            PathToSave = filePath;
        }

        public void WriteResult(string result)
        {
            File.WriteAllText(PathToSave, result);
        }
    }
}