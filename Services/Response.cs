namespace Services
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int? StatusCode { get; set; }
        public int? TotalItem { get; set; }
    }
}
