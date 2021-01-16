namespace HackathonCCR.Data
{
    public class OperationResult
    {
        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public OperationResult()
        {
            Success = true;
        }

        public OperationResult(bool success)
        {
            Success = success;
        }

        public OperationResult(string message, bool success = false)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
