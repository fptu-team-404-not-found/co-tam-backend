namespace ServiceResponse
{
    public class ServiceResponse<T>
    {
        public object Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}