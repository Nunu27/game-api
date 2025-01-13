namespace GameApi.Models;

public class RestResponseDTO<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}