namespace TodoApp.Service.DTO
{
    public class Response<T>
    {
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Payload { get; set; }
        public bool IsSuccess { get; set; }
    }
}
